namespace Lykke.Service.QuorumExplorer.Domain.DTOs
{
    /// <summary>
    ///    Smart-contract function call parameter.
    /// </summary>
    public class FunctionCallParameter
    {
        /// <summary>
        ///    The name of a function call parameter.
        /// </summary>
        public string ParameterName { get; set; }
        
        /// <summary>
        ///    The order of a function call parameter.
        /// </summary>
        public int ParameterOrder { get; set; }

        /// <summary>
        ///    The type of a function call parameter.
        /// </summary>
        public string ParameterType { get; set; }
        
        /// <summary>
        ///    The value of a function call parameter.
        /// </summary>
        public string ParameterValue { get; set; }
    }
}
