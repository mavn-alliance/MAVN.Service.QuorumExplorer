namespace Lykke.Service.QuorumExplorer.Client.Models
{
    /// <summary>
    /// Response model for transaction details
    /// </summary>
    public class TransactionDetailsResponse
    {
        /// <summary>
        /// Transaction details
        /// </summary>
        public TransactionDetailedInfo Transaction { get; set; }

        /// <summary>
        /// Error
        /// </summary>
        public TransactionsErrorCodes Error { get; set; }
    }
}
