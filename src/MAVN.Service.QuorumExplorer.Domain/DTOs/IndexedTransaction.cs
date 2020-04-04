using System.Collections.Generic;

namespace MAVN.Service.QuorumExplorer.Domain.DTOs
{
    /// <summary>
    ///    Indexed transaction DTO.
    /// </summary>
    public class IndexedTransaction
    {
        /// <summary>
        ///    The address of a created contract (if any).
        /// </summary>
        public string ContractAddress { get; set; }

        /// <summary>
        ///    The address of a transaction sender.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        ///    The input data of a transaction.
        /// </summary>
        public string Input { get; set; }

        /// <summary>
        ///    The list of a transaction logs.
        /// </summary>
        public ICollection<IndexedTransactionLog> Logs { get; set; }

        /// <summary>
        ///    The nonce of a transaction.
        /// </summary>
        public long Nonce { get; set; }

        /// <summary>
        ///    The status of a transaction.
        /// </summary>
        public long Status { get; set; }

        /// <summary>
        ///    The address of a transaction receiver.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        ///    The hash of a transaction.
        /// </summary>
        public string TransactionHash { get; set; }

        /// <summary>
        ///    The index of a transaction in a block.
        /// </summary>
        public long TransactionIndex { get; set; }
    }
}
