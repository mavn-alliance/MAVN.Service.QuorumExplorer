using System.Collections.Generic;
using JetBrains.Annotations;

namespace Lykke.Service.QuorumExplorer.Client.Models
{
    /// <summary>
    /// The function call
    /// </summary>
    [PublicAPI]
    public class FunctionCall
    {
        /// <summary>
        /// The function name
        /// </summary>
        public string FunctionName { get; set; }
        
        /// <summary>
        /// The function signature
        /// </summary>
        public string FunctionSignature { get; set; }
        
        /// <summary>
        /// The function call parameters
        /// </summary>
        public IEnumerable<FunctionCallParameter> Parameters { get; set; }
    }
}
