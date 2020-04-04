namespace MAVN.Service.QuorumExplorer.Domain.DTOs
{
    /// <summary>
    /// Solidity transaction DTO
    /// </summary>
    public class TransactionInfo
    {
        /// <summary>
        /// The hash of a corresponding block
        /// </summary>
        public string BlockHash { get; set; }
        
        /// <summary>
        /// The number of a corresponding block
        /// </summary>
        public long BlockNumber { get; set; }
        
        /// <summary>
        /// The hash of a corresponding transaction.
        /// </summary>
        public string TransactionHash { get; set; }
        
        /// <summary>
        /// The index if a corresponding transaction
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
        public long Status { get; set; }
        
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
