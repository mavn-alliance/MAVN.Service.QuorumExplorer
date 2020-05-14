using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MAVN.Common.MsSql;
using MAVN.Service.QuorumExplorer.Domain;
using MAVN.Service.QuorumExplorer.Domain.DTOs;
using MAVN.Service.QuorumExplorer.Domain.Repositories;
using MAVN.Service.QuorumExplorer.MsSqlRepositories.Contexts;
using MAVN.Service.QuorumExplorer.MsSqlRepositories.Entities;
using MAVN.Service.QuorumExplorer.MsSqlRepositories.Extensions;
using Microsoft.EntityFrameworkCore;
using Nethereum.Util;
using EventInfo = MAVN.Service.QuorumExplorer.Domain.DTOs.EventInfo;

namespace MAVN.Service.QuorumExplorer.MsSqlRepositories
{
    public class EventRepository : IEventRepository
    {
        private readonly MsSqlContextFactory<QeContext> _contextFactory;

        public EventRepository(
            MsSqlContextFactory<QeContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task SaveAsync(
            IReadOnlyCollection<Event> events, TransactionContext txContext = null)
        {
            using (var context = _contextFactory.CreateDataContext(txContext))
            {
                var hasher = Sha3Keccack.Current;

                var eventEntities = events.Select(x => new EventEntity
                {
                    EventName = x.EventName,
                    EventSignature = x.EventSignature,
                    LogIndex = x.LogIndex,
                    ParametersJson = x.ParametersJson,
                    TransactionHash = x.TransactionHash,
                    BlockTimestamp = x.BlockTimestamp
                });

                var eventParameterEntities = events
                    .SelectMany(x => x.Parameters, (x, y) => (Event: x, Parameter: y))
                    .Select(x => new EventParameterEntity
                    {
                        LogIndex = x.Event.LogIndex,
                        ParameterName = x.Parameter.ParameterName,
                        ParameterOrder = x.Parameter.ParameterOrder,
                        ParameterType = x.Parameter.ParameterType,
                        ParameterValue = x.Parameter.ParameterValue,
                        ParameterValueHash = hasher.CalculateHash(x.Parameter.ParameterValue),
                        TransactionHash = x.Event.TransactionHash
                    });

                context.Events.AddRange(eventEntities);
                context.EventParameters.AddRange(eventParameterEntities);

                await context.SaveChangesAsync();
            }
        }

        public async Task<(int total, IEnumerable<EventInfo>)> GetByBlockNumberAsync(long number, int skip, int take)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var whereResult = context.Events
                    .Include(x => x.TransactionLog)
                    .ThenInclude(x => x.Transaction)
                    .Where(x => x.TransactionLog.Transaction.BlockNumber == number);

                var total = await whereResult.CountAsync();

                var items = await whereResult
                    .Skip(skip)
                    .Take(take)
                    .Select(e => e.ToEventInfo(null))
                    .ToListAsync();

                return (total, items);
            }
        }

        public async Task<(int total, IEnumerable<EventInfo>)> GetByBlockHashAsync(string hash, int skip, int take)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var whereResult = context.Events
                    .Include(x => x.TransactionLog)
                    .ThenInclude(x => x.Transaction)
                    .Where(x => x.TransactionLog.Transaction.BlockHash == hash);

                var total = await whereResult.CountAsync();

                var items = await whereResult
                    .Skip(skip)
                    .Take(take)
                    .Select(e => e.ToEventInfo(null))
                    .ToListAsync();

                return (total, items);
            }
        }

        public async Task<(int total, IEnumerable<EventInfo>)> GetByTransactionAsync(string hash, int skip, int take)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var whereResult = context.Events
                    .Include(x => x.TransactionLog)
                    .ThenInclude(x => x.Transaction)
                    .Where(x => x.TransactionHash == hash);

                var total = await whereResult.CountAsync();

                var items = await whereResult
                    .Skip(skip)
                    .Take(take)
                    .Select(e => e.ToEventInfo(null))
                    .ToListAsync();

                return (total, items);
            }
        }

        public async Task<IEnumerable<EventInfo>> GetFilteredAsync(EventsFilter filter, int skip, int take)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var affectedAddressHashes = filter.AffectedAddresses
                    .Where(a => !string.IsNullOrWhiteSpace(a))
                    .Select(a => Sha3Keccack.Current.CalculateHash(a));

                var items = await context.Events
                    .Include(x => x.Parameters)
                    .Include(x => x.TransactionLog)
                    .ThenInclude(x => x.Transaction)
                    .Where(x =>
                        (string.IsNullOrEmpty(filter.EventName) || x.EventName == filter.EventName) &&
                        (string.IsNullOrEmpty(filter.EventSignature) || x.EventSignature == filter.EventSignature) &&
                        (string.IsNullOrEmpty(filter.Address) || x.TransactionLog.Address == filter.Address) &&
                        (!filter.AffectedAddresses.Any() || x.Parameters.Any(p =>
                             p.ParameterType == Consts.AffectedAddressParameterType &&
                             affectedAddressHashes.Contains(p.ParameterValueHash))))
                    .OrderByDescending(x => x.BlockTimestamp)
                    .Skip(skip)
                    .Take(take)
                    .Select(x => x.ToEventInfo(null))
                    .ToListAsync();

                return items;
            }
        }
    }
}
