namespace MAVN.Service.QuorumExplorer.Domain.DTOs
{
    /// <summary>
    ///    Indexed transaction log DTO.
    /// </summary>
    public class IndexedTransactionLog
    {
        /// <summary>
        ///    The address of an log originator.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///    The data of a log.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        ///    The index of a log.
        /// </summary>
        public long LogIndex { get; set; }

        /// <summary>
        ///    The list of log topics.
        /// </summary>
        public string[] Topics { get; set; }
    }
}
