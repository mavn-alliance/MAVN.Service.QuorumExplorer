using System;
using System.Threading.Tasks;
using MAVN.Service.QuorumExplorer.Domain.DTOs;
using MAVN.Service.QuorumExplorer.Domain.Repositories;
using MAVN.Service.QuorumExplorer.Domain.Services.Explorer;
using MAVN.Service.QuorumExplorer.DomainServices.Utils;

namespace MAVN.Service.QuorumExplorer.DomainServices.Explorer
{
    public class EventsReader : IEventsReader
    {
        private readonly IEventRepository _eventRepository;

        public EventsReader(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<EventsResult> GetByBlockAsync(long blockNumber, int currentPage, int pageSize)
        {
            if (blockNumber < 0)
                throw new ArgumentException( "Block number can't be negative", nameof(blockNumber));

            var (skip, take) = PagingUtils.GetNextPageParameters(currentPage, pageSize);

            var queryResult = await _eventRepository.GetByBlockNumberAsync(blockNumber, skip, take);
            
            return new EventsResult
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Events = queryResult.Item2,
                TotalCount = queryResult.total
            };
        }

        public async Task<EventsResult> GetByBlockAsync(string blockHash, int currentPage, int pageSize)
        {
            if (string.IsNullOrEmpty(blockHash))
                throw new ArgumentException( "Block hash can't be empty", nameof(blockHash));

            var (skip, take) = PagingUtils.GetNextPageParameters(currentPage, pageSize);
            
            var queryResult = await _eventRepository.GetByBlockHashAsync(blockHash, skip, take);
            
            return new EventsResult
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Events = queryResult.Item2,
                TotalCount = queryResult.total
            };
        }

        public async Task<EventsResult> GetByTransactionAsync(string txHash, int currentPage, int pageSize)
        {
            if (string.IsNullOrEmpty(txHash))
                throw new ArgumentException( "Transaction hash can't be empty", nameof(txHash));
            
            var (skip, take) = PagingUtils.GetNextPageParameters(currentPage, pageSize);
            
            var queryResult = await _eventRepository.GetByTransactionAsync(txHash, skip, take);
            
            return new EventsResult
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Events = queryResult.Item2,
                TotalCount = queryResult.total
            };
        }

        public async Task<EventsResult> GetFilteredAsync(EventsFilter filter, int currentPage, int pageSize)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));
            
            var (skip, take) = PagingUtils.GetNextPageParameters(currentPage, pageSize);

            var items = await _eventRepository.GetFilteredAsync(filter, skip, take);
            
            return new EventsResult
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Events = items
            };
        }
    }
}
