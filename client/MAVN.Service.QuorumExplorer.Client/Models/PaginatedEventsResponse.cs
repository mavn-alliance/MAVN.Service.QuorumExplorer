using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Service.QuorumExplorer.Client.Models
{
    /// <summary>
    /// Filtered events paginated response model
    /// </summary>
    [PublicAPI]
    public class PaginatedEventsResponse
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
        /// List of events for the given page
        /// </summary>
        public IEnumerable<Event> Events { get; set; }
    }
}
