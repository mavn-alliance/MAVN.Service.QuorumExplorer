using System.Collections.Generic;

namespace MAVN.Service.QuorumExplorer.Domain.DTOs
{
    public class TransactionsResult
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<TransactionInfo> Transactions { get; set; }
    }
}
