using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.QuorumExplorer.Domain.DTOs;

namespace MAVN.Service.QuorumExplorer.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<TransactionInput>> GetNonDecodedTransactionInputsAsync(int take);
        
        Task<IEnumerable<TransactionLog>> GetNonDecodedTransactionLogsAsync(int take);

        Task<(int total, IEnumerable<TransactionInfo>)> GetByBlockNumberAsync(long blockNumber, int skip, int take);
        
        Task<(int total, IEnumerable<TransactionInfo>)> GetByBlockHashAsync(string blockHash, int skip, int take);

        Task<IEnumerable<TransactionInfo>> GetFilteredAsync(TransactionsFilter filter, int skip, int take);
        
        Task<TransactionDetailedInfo> GetDetailsAsync(string hash);

        Task SaveDecodedAsync(IEnumerable<Tuple<long, string>> transactionLogs);

        Task SaveBlockTransactionsAsync(IndexedBlock indexedBlock);
    }
}
