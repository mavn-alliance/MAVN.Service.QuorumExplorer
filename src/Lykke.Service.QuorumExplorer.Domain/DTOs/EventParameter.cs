namespace Lykke.Service.QuorumExplorer.Domain.DTOs
{
    /// <summary>
    ///    Solidity event parameter DTO.
    /// </summary>
    public class EventParameter
    {
        /// <summary>
        ///    The name of an event parameter.
        /// </summary>
        public string ParameterName { get; set; }
        
        /// <summary>
        ///    The order of an event parameter.
        /// </summary>
        public int ParameterOrder { get; set; }

        /// <summary>
        ///    The type of an event parameter.
        /// </summary>
        public string ParameterType { get; set; }
        
        /// <summary>
        ///    The value of an event parameter.
        /// </summary>
        public string ParameterValue { get; set; }
    }
}
