using System;
using Autofac;
using Common;
using Lykke.Common.Log;
using MAVN.Service.QuorumExplorer.Domain.Services;

namespace Lykke.Job.QuorumExplorer.Services
{
    public class BlockchainIndexingManager : IStartable, IStopable
    {
        private readonly TimerTrigger _timerTrigger;

        public BlockchainIndexingManager(
            IBlockchainIndexingService blockchainIndexingService,
            ILogFactory logFactory)
        {
            _timerTrigger = new TimerTrigger(nameof(BlockchainIndexingManager), TimeSpan.FromSeconds(5), logFactory);
            _timerTrigger.Triggered += async (timer, args, token) =>
            {
                while (await blockchainIndexingService.IndexNonIndexedBlocksAsync()) { }
            };
        }

        public void Dispose()
        {
            _timerTrigger.Stop();
            _timerTrigger.Dispose();
        }

        public void Start()
        {
            _timerTrigger.Start();
        }

        public void Stop()
        {
            _timerTrigger.Stop();
        }
    }
}
