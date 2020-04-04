using JetBrains.Annotations;

namespace MAVN.Service.QuorumExplorer.Client.Models
{
    /// <summary>
    /// The transaction information
    /// </summary>
    [PublicAPI]
    public class Transaction
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
        /// The contract address
        /// </summary>
        public string ContractAddress { get; set; }
        
        /// <summary>
        /// The status, 0 for failed, 1 for succeeded
        /// </summary>
        public int Status { get; set; }
        
        /// <summary>
        /// The function name
        /// </summary>
        public string FunctionName { get; set; }
        
        /// <summary>
        /// The function signature
        /// </summary>
        public string FunctionSignature { get; set; }
        
        /// <summary>
        /// The timestamp
        /// </summary>
        public long Timestamp { get; set; }
    }
}
