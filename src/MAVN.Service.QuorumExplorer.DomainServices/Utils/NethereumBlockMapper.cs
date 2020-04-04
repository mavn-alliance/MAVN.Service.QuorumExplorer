using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using MAVN.Service.QuorumExplorer.Domain.DTOs;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Block = Nethereum.RPC.Eth.DTOs.Block;

namespace MAVN.Service.QuorumExplorer.DomainServices.Utils
{
    internal static class NethereumBlockMapper
    {
        [SuppressMessage("ReSharper", "ParameterTypeCanBeEnumerable.Global")]
        public static IndexedBlock MapBlock(
            Block block,
            IReadOnlyCollection<Transaction> transactions,
            IReadOnlyCollection<TransactionReceipt> receipts)
        {
            var transactionHashes = transactions.Select(x => x.TransactionHash).OrderBy(x => x);
            var receiptHashes = receipts.Select(x => x.TransactionHash).OrderBy(x => x);

            if (!transactionHashes.SequenceEqual(receiptHashes))
            {
                throw new ArgumentException("Transactions and receipts should contain corresponding data.");
            }
            
            if (transactions.Any(x => x.BlockHash != block.BlockHash))
            {
                throw new ArgumentException("All transactions should belong to the specified block.");
            }
            
            if (receipts.Any(x => x.BlockHash != block.BlockHash))
            {
                throw new ArgumentException("All receipts should belong to the specified block.");
            }
            
            var transactionsWithReceipts = transactions.Join
            (
                receipts,
                x => x.TransactionHash,
                y => y.TransactionHash,
                (x, y) => (Transaction: x, Receipt: y)
            );
            
            return new IndexedBlock
            {
                BlockHash = block.BlockHash,
                Number = (long) block.Number.Value,
                ParentHash = block.ParentHash,
                Timestamp = (long) block.Timestamp.Value,
                Transactions = MapTransactions(transactionsWithReceipts).ToList()
            };
        }

        private static IEnumerable<IndexedTransaction> MapTransactions(
            IEnumerable<(Transaction, TransactionReceipt)> transactionsWithReceipts)
        {
            foreach (var (transaction, receipt) in transactionsWithReceipts)
            {
                var receiptStatus = (long) receipt.Status.Value;
                
                yield return new IndexedTransaction
                {
                    ContractAddress = receiptStatus == 1 ? receipt.ContractAddress : null,
                    From = transaction.From,
                    Input = transaction.Input,
                    Logs = MapLogs(receipt).ToList(),
                    Nonce = (long) transaction.Nonce.Value,
                    Status = receiptStatus,
                    To = transaction.To,
                    TransactionHash = transaction.TransactionHash,
                    TransactionIndex = (long) receipt.TransactionIndex.Value
                };
            }
        }

        private static IEnumerable<IndexedTransactionLog> MapLogs(
            TransactionReceipt receipt)
        {
            foreach (var log in receipt.Logs)
            {
                yield return new IndexedTransactionLog
                {
                    Address = log.Value<string>("address"),
                    Data = log.Value<string>("data"),
                    LogIndex = (long) (new HexBigInteger(log.Value<string>("logIndex")).Value),
                    Topics = log["topics"].Values<string>().ToArray()
                };
            }
        }
    }
}
