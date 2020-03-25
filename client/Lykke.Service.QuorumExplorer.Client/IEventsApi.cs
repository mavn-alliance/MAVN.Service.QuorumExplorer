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
    public interface IEventsApi
    {
        /// <summary>
        /// Get events filtered by conditions provided
        /// </summary>
        /// <param name="request">Filters and pagination information</param>
        /// <returns></returns>
        [Post("/api/events")]
        Task<PaginatedEventsResponse> GetFilteredAsync([Body] FilteredEventsRequest request);
    }
}
