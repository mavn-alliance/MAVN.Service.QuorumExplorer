using System.Collections.Generic;

namespace Lykke.Service.QuorumExplorer.Domain.DTOs
{
    public class TransactionDetailedInfo
    {
        /// <summary>
        /// The block hash where transaction has been placed
        /// </summary>
        public string BlockHash { get; set; }
        
        /// <summary>
        /// The block number where transaction has been placed
        /// </summary>
        public long BlockNumber { get; set; }
        
        /// <summary>
        /// The transaction hash
        /// </summary>
        public string TransactionHash { get; set; }
        
        /// <summary>
        /// The transaction index
        /// </summary>
        public long TransactionIndex { get; set; }
        
        /// <summary>
        /// The address "from"
        /// </summary>
        public string From { get; set; }
        
        /// <summary>
        /// The address "to"
        /// </summary>
        public string To { get; set; }
        
        /// <summary>
        /// The status, 0 for failed, 1 for succeeded
        /// </summary>
        public long Status { get; set; }
        
        /// <summary>
        /// The input value
        /// </summary>
        public string Input { get; set; }
        
        /// <summary>
        /// The timestamp
        /// </summary>
        public long Timestamp { get; set; }
        
        /// <summary>
        /// The logs related to transaction
        /// </summary>
        public IEnumerable<TransactionLog> Logs { get; set; }
        
        /// <summary>
        /// The function call details
        /// </summary>
        public FunctionCall FunctionCall { get; set; }
        
        /// <summary>
        /// The transaction events
        /// </summary>
        public IEnumerable<EventInfo> Events { get; set; }
    }
}
