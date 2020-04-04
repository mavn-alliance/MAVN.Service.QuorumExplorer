using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace MAVN.Service.QuorumExplorer.Client.Models
{
    /// <summary>
    /// Get filtered events request details
    /// </summary>
    [PublicAPI]
    public class FilteredEventsRequest
    {
        /// <summary>
        /// The event name
        /// </summary>
        public string EventName { get; set; }
        
        /// <summary>
        /// The event signature
        /// </summary>
        public string EventSignature { get; set; }
        
        /// <summary>
        /// The contract address
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// Filter events by affected addresses
        /// </summary>
        [Required]
        public IEnumerable<string> AffectedAddresses { get; set; }
        
        /// <summary>
        /// Pagination information
        /// </summary>
        [Required]
        public PaginationModel PagingInfo { get; set; }
    }
}
