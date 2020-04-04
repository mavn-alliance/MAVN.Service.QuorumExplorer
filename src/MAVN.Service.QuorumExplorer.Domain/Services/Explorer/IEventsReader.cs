using System.Threading.Tasks;
using MAVN.Service.QuorumExplorer.Domain.DTOs;

namespace MAVN.Service.QuorumExplorer.Domain.Services.Explorer
{
    public interface IEventsReader
    {
        Task<EventsResult> GetByBlockAsync(long blockNumber, int currentPage, int pageSize);

        Task<EventsResult> GetByBlockAsync(string blockHash, int currentPage, int pageSize);
        
        Task<EventsResult> GetByTransactionAsync(string txHash, int currentPage, int pageSize);
        
        Task<EventsResult> GetFilteredAsync(EventsFilter filter, int currentPage, int pageSize);
    }
}
