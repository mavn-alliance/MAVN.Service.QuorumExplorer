using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Service.QuorumExplorer.Client.Models
{
    /// <summary>
    /// The transaction log
    /// </summary>
    [PublicAPI]
    public class TransactionLog
    {
        /// <summary>
        /// The address of the contract
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        /// The log index
        /// </summary>
        public long LogIndex { get; set; }
        
        /// <summary>
        /// The log data json-serialized
        /// </summary>
        public string Data { get; set; }
        
        /// <summary>
        /// The array of topics
        /// </summary>
        public IEnumerable<string> Topics { get; set; }
    }
}
