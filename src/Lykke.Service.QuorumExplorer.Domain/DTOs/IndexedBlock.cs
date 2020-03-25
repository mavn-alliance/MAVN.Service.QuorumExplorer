using System.Collections.Generic;

namespace Lykke.Service.QuorumExplorer.Domain.DTOs
{
    /// <summary>
    ///    Indexed block DTO.
    /// </summary>
    public class IndexedBlock
    {
        /// <summary>
        ///    The hash of a block.
        /// </summary>
        public string BlockHash { get; set; }

        /// <summary>
        ///    The number of a block.
        /// </summary>
        public long Number { get; set; }
        
        /// <summary>
        ///    The hash of a parent block.
        /// </summary>
        public string ParentHash { get; set; }

        /// <summary>
        ///    The timestamp of a block.
        /// </summary>
        public long Timestamp { get; set; }
        
        /// <summary>
        ///    The list of a block transactions.
        /// </summary>
        public ICollection<IndexedTransaction> Transactions { get; set; }
    }
}
