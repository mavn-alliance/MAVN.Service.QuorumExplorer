using System;
using System.Threading.Tasks;
using Common;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Sdk;

namespace Lykke.Job.QuorumExplorer.Services
{
    public class ShutdownManager : IShutdownManager
    {
        private readonly IStopable _blockchainIndexingManager;
        private readonly ILog _log;
        private readonly IStopable _transactionInputDecodingManager;
        private readonly IStopable _transactionLogDecodingManager;

        public ShutdownManager(
            IStopable blockchainIndexingManager,
            ILogFactory logFactory,
            IStopable transactionInputDecodingManager,
            IStopable transactionLogDecodingManager)
        {
            _blockchainIndexingManager = blockchainIndexingManager;
            _log = logFactory.CreateLog(this);
            _transactionInputDecodingManager = transactionInputDecodingManager;
            _transactionLogDecodingManager = transactionLogDecodingManager;
        }

        public async Task StopAsync()
        {
            await StopServiceAsync(_blockchainIndexingManager, "Blockchain Indexing Manager");
            await StopServiceAsync(_transactionInputDecodingManager, "Transaction Input Decoding Manager");
            await StopServiceAsync(_transactionLogDecodingManager, "Transaction Log Decoding Manager");
        }

        private Task StopServiceAsync(
            IStopable service,
            string serviceName)
        {
            try
            {
                #region Logging
            
                _log.Info($"{serviceName} is shutting down.");
            
                #endregion
            
                service.Stop();
            
                #region Logging
            
                _log.Info($"{serviceName} has been shutdown.");
            
                #endregion
            }
            catch (Exception)
            {
                #region Logging
                
                _log.Warning($"{serviceName} shutdown failed.");
                
                #endregion
            }

            return Task.CompletedTask;
        }
    }
}
