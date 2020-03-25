using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.QuorumExplorer.Client.Models;
using Refit;

namespace Lykke.Service.QuorumExplorer.Client
{
    /// <summary>
    /// Transactions API interface
    /// </summary>
    [PublicAPI]
    public interface ITransactionsApi
    {
        /// <summary>
        /// Get transactions filtered by conditions provided
        /// </summary>
        /// <param name="request">Filters and pagination information</param>
        /// <returns></returns>
        [Post("/api/transactions")]
        Task<PaginatedTransactionsResponse> GetFilteredAsync([Body] FilteredTransactionsRequest request);
        
        /// <summary>
        /// Get events related to transaction
        /// </summary>
        /// <param name="request">Transaction hash and paging details</param>
        /// <returns></returns>
        [Get("/api/transactions/events")]
        Task<PaginatedEventsResponse> GetEventsByTransactionAsync([Query] TransactionEventsRequest request);

        /// <summary>
        /// Get transaction details by hash
        /// </summary>
        /// <param name="transactionHash">The transaction hash</param>
        /// <returns></returns>
        [Get("/api/transactions/{transactionHash}")]
        Task<TransactionDetailsResponse> GetDetailsAsync(string transactionHash);
    }
}
