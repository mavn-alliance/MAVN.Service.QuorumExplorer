using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Service.QuorumExplorer.Client.Models
{
    /// <summary>
    /// Filtered transactions paginated response model
    /// </summary>
    [PublicAPI]
    public class PaginatedTransactionsResponse
    {
        /// <summary>
        /// Current page in pagination result
        /// </summary>
        public int CurrentPage { get; set; }
        
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }
        
        /// <summary>
        /// Total count of records
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// List of blocks for the given page
        /// </summary>
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
