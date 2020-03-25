using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Common.MsSql;
using Lykke.Service.QuorumExplorer.Domain;
using Lykke.Service.QuorumExplorer.Domain.DTOs;
using Lykke.Service.QuorumExplorer.Domain.Extensions;
using Lykke.Service.QuorumExplorer.Domain.Repositories;
using Lykke.Service.QuorumExplorer.MsSqlRepositories.Contexts;
using Lykke.Service.QuorumExplorer.MsSqlRepositories.Entities;
using Lykke.Service.QuorumExplorer.MsSqlRepositories.Extensions;
using Microsoft.EntityFrameworkCore;
using Nethereum.Util;

namespace Lykke.Service.QuorumExplorer.MsSqlRepositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly MsSqlContextFactory<QeContext> _contextFactory;
        
        public TransactionRepository(
            MsSqlContextFactory<QeContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<TransactionInput>> GetNonDecodedTransactionInputsAsync(int take)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var transactions = await context.Transactions
                    .Join(
                        context.ABIs,
                        o => o.InputSignature,
                        i => i.Signature,
                        (t, abi) => new {t.Input, t.TransactionHash, t.FunctionCall, abi.Abi})
                    .Where(t => t.FunctionCall == null)
                    .Select(t => new {t.Abi, t.Input, t.TransactionHash})
                    .Take(take)
                    .ToListAsync();

                return transactions.Select(x => new TransactionInput
                {
                    Abi = x.Abi, EncodedInput = x.Input, TransactionHash = x.TransactionHash
                });
            }
        }

        public async Task<IEnumerable<TransactionLog>> GetNonDecodedTransactionLogsAsync(int take)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var transactionLogs = await context.TransactionLogs
                    .Where(t => t.Decoded == false)
                    .Join(
                        context.ABIs,
                        o => o.Topic0,
                        i => i.Signature,
                        (tl, abi) => new
                        {
                            tl.TransactionHash,
                            Topics = tl.GetTopics(),
                            tl.LogIndex,
                            tl.Data,
                            tl.Event,
                            tl.BlockTimestamp,
                            abi.Abi
                        })
                    .Select(t => new
                    {
                        t.Abi,
                        t.Data,
                        t.LogIndex,
                        t.Topics,
                        t.TransactionHash,
                        t.BlockTimestamp
                    })
                    .Take(take)
                    .ToListAsync();
                
                return transactionLogs.Select(x => new TransactionLog
                {
                    Abi = x.Abi,
                    Data = x.Data,
                    LogIndex = x.LogIndex,
                    Topics = x.Topics.ToArray(),
                    TransactionHash = x.TransactionHash,
                    BlockTimestamp = x.BlockTimestamp
                });
            }
        }

        public async Task<IEnumerable<TransactionInfo>> GetFilteredAsync(TransactionsFilter filter, int skip, int take)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var hasher = Sha3Keccack.Current;
                
                var items = await context.Transactions
                    .Include(x => x.FunctionCall)
                    .Where(tx =>
                        (!filter.From.Any() || filter.From.Contains(tx.From)) &&
                        (!filter.To.Any() || filter.To.Contains(tx.To)) &&
                        (string.IsNullOrEmpty(filter.FunctionName) ||
                         tx.FunctionCall.FunctionName == filter.FunctionName) &&
                        (filter.FunctionSignature == null ||
                         tx.FunctionCall.FunctionSignature == filter.FunctionSignature) &&
                        (!filter.AffectedAddresses.Any() || tx.FunctionCall.Parameters
                             .Where(x => x.ParameterType == Consts.AffectedAddressParameterType)
                             .Any(p => filter.AffectedAddresses.Select(a => hasher.CalculateHash(a)).Contains(p.ParameterValueHash))))
                    .Skip(skip)
                    .Take(take)
                    .Select(x => x.ToTransactionInfo())
                    .ToListAsync();

                return items;
            }
        }

        public async Task<TransactionDetailedInfo> GetDetailsAsync(string hash)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var result = await context.Transactions
                    .Include(x => x.FunctionCall)
                    .ThenInclude(x => x.Parameters)
                    .Include(x => x.Logs)
                    .Where(x => x.TransactionHash == hash)
                    .GroupJoin(
                        context.Events,
                        t => t.TransactionHash,
                        e => e.TransactionHash,
                        (t, events) => new {Transaction = t, Events = events})
                    .SelectMany(
                        x => x.Events.DefaultIfEmpty(),
                        (x, y) => new {x.Transaction, Event = y})
                    .ToListAsync();

                var tx = result.FirstOrDefault()?.Transaction;

                if (tx == null)
                    return null;

                var eventParameters = await context.EventParameters
                    .Where(x => x.TransactionHash == hash)
                    .ToListAsync();

                return new TransactionDetailedInfo
                {
                    BlockHash = tx.BlockHash,
                    BlockNumber = tx.BlockNumber,
                    TransactionHash = tx.TransactionHash,
                    TransactionIndex = tx.TransactionIndex,
                    From = tx.From,
                    To = tx.To,
                    Input = tx.Input,
                    Status = tx.Status,
                    Timestamp = tx.BlockTimestamp,
                    FunctionCall = tx.FunctionCall?.ToDto(),
                    Logs = tx.Logs?.Select(l => l.ToDto()) ?? Enumerable.Empty<TransactionLog>(),
                    Events = result.Select(e => e.Event?.ToEventInfo(eventParameters)).Where(x => x != null)
                };
            }
        }

        public async Task SaveDecodedAsync(IEnumerable<Tuple<long, string>> transactionLogs)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                foreach (var txLog in transactionLogs)
                {
                    var e = new TransactionLogEntity
                    {
                        LogIndex = txLog.Item1, TransactionHash = txLog.Item2
                    };
                    
                    context.TransactionLogs.Attach(e);

                    e.Decoded = true;
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<(int total, IEnumerable<TransactionInfo>)> GetByBlockNumberAsync(long blockNumber, int skip, int take)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var query = context.Transactions
                    .Include(x => x.FunctionCall)
                    .Where(x => x.BlockNumber == blockNumber);

                var total = await query.CountAsync();
                
                var items = await query
                    .Skip(skip)
                    .Take(take)
                    .Select(x => x.ToTransactionInfo())
                    .ToListAsync();

                return (total, items);
            }
        }

        public async Task<(int total, IEnumerable<TransactionInfo>)> GetByBlockHashAsync(string blockHash, int skip, int take)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var query = context.Transactions
                    .Include(x => x.FunctionCall)
                    .Where(x => x.BlockHash == blockHash);

                var total = await query.CountAsync();
                
                var items = await query
                    .Skip(skip)
                    .Take(take)
                    .Select(x => x.ToTransactionInfo())
                    .ToListAsync();

                return (total, items);
            }
        }
        
        public async Task SaveBlockTransactionsAsync(IndexedBlock indexedBlock)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var transactionEntities = indexedBlock.Transactions
                    .Select(x => new TransactionEntity
                    {
                        BlockHash = indexedBlock.BlockHash,
                        BlockNumber = indexedBlock.Number,
                        BlockTimestamp = indexedBlock.Timestamp,
                        ContractAddress = x.ContractAddress,
                        From = x.From,
                        Input = x.Input,
                        InputSignature = x.GetInputSignature(),
                        Nonce = x.Nonce,
                        Status = x.Status,
                        To = x.To,
                        TransactionHash = x.TransactionHash,
                        TransactionIndex = x.TransactionIndex
                    });
                
                var transactionLogEntities = indexedBlock.Transactions
                    .SelectMany(x => x.Logs, (x, y) => (Transaction: x, Log: y))
                    .Select(x => new TransactionLogEntity
                    {
                        Address = x.Log.Address,
                        Data = x.Log.Data,
                        LogIndex = x.Log.LogIndex,
                        Topic0 = x.Log.Topics[0],
                        Topic1 = x.Log.Topics.Length >= 2 ? x.Log.Topics[1] : null,
                        Topic2 = x.Log.Topics.Length >= 3 ? x.Log.Topics[2] : null,
                        Topic3 = x.Log.Topics.Length >= 4 ? x.Log.Topics[3] : null,
                        TransactionHash = x.Transaction.TransactionHash,
                        Decoded = false,
                        BlockTimestamp = indexedBlock.Timestamp
                    });
                
                context.Transactions.AddRange(transactionEntities);
                context.TransactionLogs.AddRange(transactionLogEntities);
                
                await context.SaveChangesAsync();
            }
        }
    }
}
