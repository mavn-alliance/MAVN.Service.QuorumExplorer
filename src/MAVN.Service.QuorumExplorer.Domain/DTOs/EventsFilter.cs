using System.Collections.Generic;

namespace MAVN.Service.QuorumExplorer.Domain.DTOs
{
    public class EventsFilter
    {
        public string EventName { get; set; }
        
        public string EventSignature { get; set; }
        
        public string Address { get; set; }
        
        public IEnumerable<string> AffectedAddresses { get; set; }
    }
}
