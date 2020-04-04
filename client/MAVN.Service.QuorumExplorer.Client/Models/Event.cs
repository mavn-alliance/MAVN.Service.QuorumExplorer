using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Service.QuorumExplorer.Client.Models
{
    /// <summary>
    /// The event information
    /// </summary>
    [PublicAPI]
    public class Event
    {
        /// <summary>
        /// The block hash where transaction related to the event has been placed
        /// </summary>
        public string BlockHash { get; set; }
        
        /// <summary>
        /// The block number where transaction related to the event has been placed
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
        /// The log index
        /// </summary>
        public long LogIndex { get; set; }
        
        /// <summary>
        /// The event address
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// The event name
        /// </summary>
        public string EventName { get; set; }
        
        /// <summary>
        /// The event signature
        /// </summary>
        public string EventSignature { get; set; }
        
        /// <summary>
        /// The timestamp
        /// </summary>
        public long Timestamp { get; set; }
        
        /// <summary>
        /// The list of the event parameters
        /// </summary>
        public IEnumerable<EventParameter> Parameters { get; set; }
    }
}
