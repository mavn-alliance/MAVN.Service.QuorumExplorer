using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Service.QuorumExplorer.Domain.DTOs;
using Lykke.Service.QuorumExplorer.Domain.Repositories;
using Lykke.Service.QuorumExplorer.Domain.Services;
using Lykke.Service.QuorumExplorer.DomainServices.Extensions;
using Lykke.Service.QuorumExplorer.DomainServices.Utils;
using Nethereum.JsonRpc.Client;
using Nethereum.Web3;

namespace Lykke.Service.QuorumExplorer.DomainServices
{
    public class BlockchainIndexingService : IBlockchainIndexingService
    {
        private readonly ILog _log;
        private readonly IWeb3 _web3;
        private readonly IIndexingStateRepository _indexingStateRepository;
        private readonly ITransactionRepository _transactionRepository;

        public BlockchainIndexingService(
            ILogFactory logFactory,
            IWeb3 web3, 
            IIndexingStateRepository indexingStateRepository, 
            ITransactionRepository transactionRepository)
        {
            _log = logFactory.CreateLog(this);
            _web3 = web3;
            _indexingStateRepository = indexingStateRepository;
            _transactionRepository = transactionRepository;
        }

        
        public async Task<bool> IndexNonIndexedBlocksAsync()
        {
            using (_log.BeginScope($"{nameof(IndexNonIndexedBlocksAsync)}-{Guid.NewGuid()}"))
            {
                (long From, long To)? nonIndexedBlocksRange;
                
                try
                {
                    #region Logging
                    
                    _log.Debug("Getting non-indexed blocks range.");
                    
                    #endregion
                    
                    nonIndexedBlocksRange = await TryGetNonIndexedBlocksRangeAsync();
                }
                catch (RpcClientUnknownException e) when (e.InnerException is OperationCanceledException)
                {
                    #region Logging

                    _log.Warning
                    (
                        $"Failed to get next non-indexed block number. Quorum RPC node is unresponsive.",
                        e
                    );

                    #endregion

                    return false;
                }
                catch (Exception e)
                {
                    #region Logging

                    _log.Warning("Failed to get next non-indexed block number.", e);

                    #endregion

                    return false;
                }

                if (!nonIndexedBlocksRange.HasValue)
                {
                    #region Logging

                    _log.Debug("No new blocks have been found.");

                    #endregion
                    
                    return false;
                }

                var (nonIndexedBlocksRangeFrom, nonIndexedBlocksRangeTo) = nonIndexedBlocksRange.Value;
                
                #region Logging
                    
                _log.Debug($"Got non-indexed block range [{nonIndexedBlocksRangeFrom}..{nonIndexedBlocksRangeTo}].");
                    
                #endregion

                for (var i = nonIndexedBlocksRangeFrom; i <= nonIndexedBlocksRangeTo; i++)
                {
                    try
                    {
                        #region Logging

                        _log.Debug
                        (
                            $"Indexing block [{i}].",
                            new { BlockNumber = i }
                        );

                        #endregion

                        var indexedBlock = await IndexBlockAsync(i);

                        #region Logging

                        _log.Info
                        (
                            $"Block [{i}] has been indexed.",
                            new
                            {
                                indexedBlock.BlockHash,
                                BlockNumber = indexedBlock.Number,
                                TransactionsCount = indexedBlock.Transactions.Count,
                                TransactionLogsCount = indexedBlock.Transactions.SelectMany(x => x.Logs).Count()
                            }
                        );

                        #endregion
                    }
                    catch (RpcClientUnknownException e) when (e.InnerException is OperationCanceledException)
                    {
                        #region Logging

                        _log.Warning
                        (
                            $"Failed to index block [{i}]. Quorum RPC node is unresponsive.",
                            e,
                            new { BlockNumber = i }
                        );

                        #endregion

                        return false;
                    }
                    catch (Exception e)
                    {
                        #region Logging

                        _log.Error
                        (
                            e,
                            $"Failed to index block [{i}].",
                            new { BlockNumber = i }
                        );

                        #endregion

                        return false;
                    }
                }

                return true;
            }
        }
        
        private async Task<IndexedBlock> IndexBlockAsync(
            long blockNumber)
        {
            var block = await _web3.Eth.Blocks.GetBlockWithTransactionsHashesByNumber.SendRequestAsync(blockNumber);

            #region Logging

            _log.Debug
            (
                $"Got block [{blockNumber}] from RPC node.",
                new { BlockNumber = blockNumber }
            );

            #endregion

            // workaround to decrease the num,ber of parallel calls to blockchain node 
            var transactions = await _web3.Eth.Transactions.GetTransactionsAsync(block.TransactionHashes);
            var receipts = await _web3.Eth.Transactions.GetReceiptsAsync(block.TransactionHashes);

            #region Logging

            _log.Debug
            (
                $"Got {block.TransactionHashes.Length} transactions and receipts for block [{blockNumber}] from RPC node.",
                new {BlockNumber = blockNumber}
            );

            #endregion

            var indexedBlock = NethereumBlockMapper.MapBlock(block, transactions, receipts);

            await Task.WhenAll(
                _transactionRepository.SaveBlockTransactionsAsync(indexedBlock),
                _indexingStateRepository.SaveLastIndexedBlockNumber(indexedBlock.Number));

            #region Logging

            _log.Debug
            (
                $"Block [{blockNumber}] has been saved to DB.",
                new {BlockNumber = blockNumber}
            );

            #endregion
            
            return indexedBlock;
        }



        private async Task<(long From, long To)?> TryGetNonIndexedBlocksRangeAsync()
        {
            var getBestBlockNumber = _web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();

            var getBestIndexedBlockNumber = _indexingStateRepository.GetLastIndexedBlockNumberAsync();

            await Task.WhenAll(getBestBlockNumber, getBestIndexedBlockNumber);
            
            var bestBlockNumber = (long) getBestBlockNumber.Result.Value;
            var bestIndexedBlockNumber = getBestIndexedBlockNumber.Result ?? -1;
            
            #region Logging

            _log.Debug
            (
                $"Got best block number [{bestBlockNumber}] from RPC node and best indexed block number [{bestIndexedBlockNumber}] from DB.",
                new {BestBlockNumber = bestBlockNumber, BestIndexedBlockNumber = bestIndexedBlockNumber}
            );
            
            #endregion
            
            if (bestIndexedBlockNumber < bestBlockNumber)
            {
                return (bestIndexedBlockNumber + 1, bestBlockNumber);
            }
            else
            {
                return null;
            }
        }
    }
}
