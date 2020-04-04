using System;
using System.Threading.Tasks;
using Autofac;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Sdk;

namespace Lykke.Job.QuorumExplorer.Services
{
    public class StartupManager : IStartupManager
    {
        private readonly IStartable _blockchainIndexingManager;
        private readonly ILog _log;
        private readonly IStartable _transactionInputDecodingManager;
        private readonly IStartable _transactionLogDecodingManager;

        public StartupManager(
            IStartable blockchainIndexingManager,
            ILogFactory logFactory,
            IStartable transactionInputDecodingManager,
            IStartable transactionLogDecodingManager)
        {
            _blockchainIndexingManager = blockchainIndexingManager;
            _log = logFactory.CreateLog(this);
            _transactionInputDecodingManager = transactionInputDecodingManager;
            _transactionLogDecodingManager = transactionLogDecodingManager;
        }

        public async Task StartAsync()
        {
            StartService(_blockchainIndexingManager, "Blockchain Indexing Manager");
            StartService(_transactionInputDecodingManager, "Transaction Input Decoding Manager");
            StartService(_transactionLogDecodingManager, "Transaction Log Decoding Manager");
            
            await Task.CompletedTask;
        }
        
        private void StartService(
            IStartable service,
            string serviceName)
        {
            try
            {
                #region Logging
                
                _log.Info($"{serviceName} is starting.");
                
                #endregion

                service.Start();
                
                #region Logging
                
                _log.Info($"{serviceName} has been started.");
                
                #endregion
            }
            catch (Exception e)
            {
                #region Logging
                
                _log.Critical(e, $"{serviceName} starting failed.");
                
                #endregion
                
                throw;
            }
        }
    }
}
