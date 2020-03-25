namespace Lykke.Service.QuorumExplorer.Domain.DTOs
{
    /// <summary>
    ///    ABI DTO.
    /// </summary>
    public class ABI
    {
        /// <summary>
        ///    The body of an ABI.
        /// </summary>
        public string Abi { get; set; }
        
        /// <summary>
        ///    The name of an ABI.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        ///    The signature of an ABI.
        /// </summary>
        public string Signature { get; set; }
        
        /// <summary>
        ///    The type of an ABI.
        /// </summary>
        public ABIType Type { get; set; }
    }
}
