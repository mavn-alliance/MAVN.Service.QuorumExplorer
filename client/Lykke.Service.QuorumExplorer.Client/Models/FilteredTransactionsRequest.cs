using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Lykke.Service.QuorumExplorer.Client.Models
{
    /// <summary>
    /// Get filtered transactions request details
    /// </summary>
    [PublicAPI]
    public class FilteredTransactionsRequest
    {
        /// <summary>
        /// Filter transactions by function name 
        /// </summary>
        public string FunctionName { get; set; }
        
        /// <summary>
        /// Filter transactions by function signature
        /// </summary>
        public string FunctionSignature { get; set; }
        
        /// <summary>
        /// Filter transactions by address "from"
        /// </summary>
        [Required]
        public IEnumerable<string> From { get; set; }
        
        /// <summary>
        /// Filter transactions by address "to"
        /// </summary>
        [Required]
        public IEnumerable<string> To { get; set; }
        
        /// <summary>
        /// Filter transactions by affected addresses
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
