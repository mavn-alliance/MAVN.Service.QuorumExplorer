using System.Threading.Tasks;
using Lykke.Service.QuorumExplorer.Domain.DTOs;

namespace Lykke.Service.QuorumExplorer.Domain.Services.Explorer
{
    public interface ITransactionsReader
    {
        Task<TransactionsResult> GetFilteredAsync(TransactionsFilter filter, int currentPage, int pageSize);

        Task<TransactionsResult> GetByBlockAsync(long blockNumber, int currentPage, int pageSize);

        Task<TransactionsResult> GetByBlockAsync(string blockHash, int currentPage, int pageSize);

        Task<TransactionDetailedInfo> GetDetailsAsync(string transactionHash);
    }
}
