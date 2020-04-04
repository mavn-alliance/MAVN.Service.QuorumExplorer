namespace MAVN.Service.QuorumExplorer.Domain.DTOs
{
    /// <summary>
    ///    Transaction log DTO.
    /// </summary>
    public class TransactionLog
    {
        /// <summary>
        ///    The ABI of a related solidity event.
        /// </summary>
        public string Abi { get; set; }
        
        /// <summary>
        ///    The data of a transaction log.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        ///    The index of a transaction log.
        /// </summary>
        public long LogIndex { get; set; }

        /// <summary>
        ///    The list of a transaction log topics.
        /// </summary>
        public string[] Topics { get; set; }
        
        /// <summary>
        ///    The hash of a corresponding transaction.
        /// </summary>
        public string TransactionHash { get; set; }
        
        /// <summary>
        ///     The address of the contract
        /// </summary>
        public string Address { get; set; }
        
        /// <summary>
        ///     The parent block timestamp
        /// </summary>
        public long BlockTimestamp { get; set; }
    }
}
