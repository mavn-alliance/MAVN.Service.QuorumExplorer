using System.Collections.Generic;

namespace Lykke.Service.QuorumExplorer.Domain.DTOs
{
    public class EventsResult
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<EventInfo> Events { get; set; }
    }
}
