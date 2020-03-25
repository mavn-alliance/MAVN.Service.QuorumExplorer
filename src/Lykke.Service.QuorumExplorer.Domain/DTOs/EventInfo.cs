using System.Collections.Generic;

namespace Lykke.Service.QuorumExplorer.Domain.DTOs
{
    /// <summary>
    /// Solidity event DTO with extended information.
    /// </summary>
    public class EventInfo
    {
        /// <summary>
        ///     The hash of a corresponding block
        /// </summary>
        public string BlockHash { get; set; }
        
        /// <summary>
        ///     The number of a corresponding block
        /// </summary>
        public long BlockNumber { get; set; }
        
        /// <summary>
        ///    The hash of a corresponding transaction.
        /// </summary>
        public string TransactionHash { get; set; }
        
        /// <summary>
        ///     The index if a corresponding transaction
        /// </summary>
        public long TransactionIndex { get; set; }
        
        /// <summary>
        ///    The index of a corresponding transaction log.
        /// </summary>
        public long LogIndex { get; set; }
        
        /// <summary>
        ///    The address of the contract
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        ///    The name of an event.
        /// </summary>
        public string EventName { get; set; }
        
        /// <summary>
        ///    The signature of an event.
        /// </summary>
        public string EventSignature { get; set; }

        /// <summary>
        ///    The list of an event parameters.
        /// </summary>
        public ICollection<EventParameter> Parameters { get; set; }
        
        /// <summary>
        /// The timestamp of a corresponding block
        /// </summary>
        public long Timestamp { get; set; }
    }
}
