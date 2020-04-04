using System.Collections.Generic;

namespace MAVN.Service.QuorumExplorer.Domain.DTOs
{
    /// <summary>
    ///    Smart contract function call.
    /// </summary>
    public class FunctionCall
    {
        /// <summary>
        ///    The name of a smart-contract function.
        /// </summary>
        public string FunctionName { get; set; }

        /// <summary>
        ///    The signature of a smart-contract function.
        /// </summary>
        public string FunctionSignature { get; set; }

        /// <summary>
        ///    The list of a call parameters.
        /// </summary>
        public ICollection<FunctionCallParameter> Parameters { get; set; }
        
        /// <summary>
        ///    The list of a call parameters represented as a JSON.
        /// </summary>
        public string ParametersJson { get; set; }

        /// <summary>
        ///    The hash of a corresponding transaction.
        /// </summary>
        public string TransactionHash { get; set; }
    }
}
