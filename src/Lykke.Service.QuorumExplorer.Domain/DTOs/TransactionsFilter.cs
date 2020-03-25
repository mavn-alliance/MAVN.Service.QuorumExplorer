using System.Collections.Generic;

namespace Lykke.Service.QuorumExplorer.Domain.DTOs
{
    public class TransactionsFilter
    {
        public string FunctionName { get; set; }
        public string FunctionSignature { get; set; }
        public IEnumerable<string> From { get; set; }
        public IEnumerable<string> To { get; set; }
        public IEnumerable<string> AffectedAddresses { get; set; }
    }
}
