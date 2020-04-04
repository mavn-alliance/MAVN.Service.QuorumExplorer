namespace MAVN.Service.QuorumExplorer.Domain.DTOs
{
    /// <summary>
    ///    Transaction input DTO.
    /// </summary>
    public class TransactionInput
    {
        /// <summary>
        ///    The ABI of a related smart-contract function.
        /// </summary>
        public string Abi { get; set; }
        
        /// <summary>
        ///    The input of a transaction, as it represented in a blockchain.
        /// </summary>
        public string EncodedInput { get; set; }
        
        /// <summary>
        ///    The hash of a corresponding transaction.
        /// </summary>
        public string TransactionHash { get; set; }
    }
}
