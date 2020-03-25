using System.Collections.Generic;

namespace Lykke.Service.QuorumExplorer.Domain.DTOs
{
    /// <summary>
    ///    Solidity event DTO.
    /// </summary>
    public class Event
    {
        /// <summary>
        ///    The name of an event.
        /// </summary>
        public string EventName { get; set; }
        
        /// <summary>
        ///    The signature of an event.
        /// </summary>
        public string EventSignature { get; set; }
        
        /// <summary>
        ///    The index of a corresponding transaction log.
        /// </summary>
        public long LogIndex { get; set; }

        /// <summary>
        ///    The list of an event parameters.
        /// </summary>
        public ICollection<EventParameter> Parameters { get; set; }
        
        /// <summary>
        ///    The list of event parameters represented as a json.
        /// </summary>
        public string ParametersJson { get; set; }

        /// <summary>
        ///    The hash of a corresponding transaction.
        /// </summary>
        public string TransactionHash { get; set; }
        
        /// <summary>
        ///    The parent block timestamp
        /// </summary>
        public long BlockTimestamp { get; set; }
    }
}
