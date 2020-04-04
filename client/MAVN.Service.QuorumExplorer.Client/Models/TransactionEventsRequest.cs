using System.ComponentModel.DataAnnotations;

namespace MAVN.Service.QuorumExplorer.Client.Models
{
    /// <summary>
    /// Holds pagination information and transaction hash
    /// </summary>
    public class TransactionEventsRequest : PaginationModel
    {
        /// <summary>
        /// The transaction hash
        /// </summary>
        [Required]
        public string TransactionHash { get; set; }        
    }
}
