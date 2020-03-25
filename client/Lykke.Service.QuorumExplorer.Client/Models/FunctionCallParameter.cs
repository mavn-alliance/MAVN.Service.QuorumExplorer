using JetBrains.Annotations;

namespace Lykke.Service.QuorumExplorer.Client.Models
{
    /// <summary>
    /// The function parameter
    /// </summary>
    [PublicAPI]
    public class FunctionCallParameter
    {
        /// <summary>
        /// The name of the parameter
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        ///  The order of the parameter
        /// </summary>
        public int Order { get; set; }
        
        /// <summary>
        /// The type of the parameter
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// The value of the parameter
        /// </summary>
        public string Value { get; set; }
    }
}
