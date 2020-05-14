using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Common.MsSql;
using MAVN.Service.QuorumExplorer.Domain.DTOs;

namespace MAVN.Service.QuorumExplorer.Domain.Repositories
{
    public interface IEventRepository
    {
        Task SaveAsync(IReadOnlyCollection<Event> events, TransactionContext txContext = null);

        Task<(int total, IEnumerable<EventInfo>)> GetByBlockNumberAsync(long number, int skip, int take);

        Task<(int total, IEnumerable<EventInfo>)> GetByBlockHashAsync(string hash, int skip, int take);

        Task<(int total, IEnumerable<EventInfo>)> GetByTransactionAsync(string hash, int skip, int take);

        Task<IEnumerable<EventInfo>> GetFilteredAsync(EventsFilter filter, int skip, int take);
    }
}
