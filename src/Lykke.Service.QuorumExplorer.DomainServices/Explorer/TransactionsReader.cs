using System;
using System.Threading.Tasks;
using Lykke.Service.QuorumExplorer.Domain.DTOs;
using Lykke.Service.QuorumExplorer.Domain.Repositories;
using Lykke.Service.QuorumExplorer.Domain.Services.Explorer;
using Lykke.Service.QuorumExplorer.DomainServices.Utils;

namespace Lykke.Service.QuorumExplorer.DomainServices.Explorer
{
    public class TransactionsReader : ITransactionsReader
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsReader(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionsResult> GetFilteredAsync(TransactionsFilter filter, int currentPage, int pageSize)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));
            
            var (skip, take) = PagingUtils.GetNextPageParameters(currentPage, pageSize);

            var items = await _transactionRepository.GetFilteredAsync(filter, skip, take);
            
            return new TransactionsResult
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Transactions = items
            };
        }

        public async Task<TransactionsResult> GetByBlockAsync(long blockNumber, int currentPage, int pageSize)
        {
            if (blockNumber < 0)
                throw new ArgumentException( "Block number can't be negative", nameof(blockNumber));
            
            var (skip, take) = PagingUtils.GetNextPageParameters(currentPage, pageSize);

            var queryResult = await _transactionRepository.GetByBlockNumberAsync(blockNumber, skip, take);
            
            return new TransactionsResult
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Transactions = queryResult.Item2,
                TotalCount = queryResult.total
            };
        }

        public async Task<TransactionsResult> GetByBlockAsync(string blockHash, int currentPage, int pageSize)
        {
            if (string.IsNullOrEmpty(blockHash))
                throw new ArgumentException( "Block hash can't be empty", nameof(blockHash));
            
            var (skip, take) = PagingUtils.GetNextPageParameters(currentPage, pageSize);

            var queryResult = await _transactionRepository.GetByBlockHashAsync(blockHash, skip, take);
            
            return new TransactionsResult
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                Transactions = queryResult.Item2,
                TotalCount = queryResult.total
            };
        }

        public Task<TransactionDetailedInfo> GetDetailsAsync(string transactionHash)
        {
            if (string.IsNullOrEmpty(transactionHash))
                throw new ArgumentNullException(nameof(transactionHash));

            return _transactionRepository.GetDetailsAsync(transactionHash);
        }
    }
}
