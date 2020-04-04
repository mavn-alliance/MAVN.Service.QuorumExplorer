using Autofac;
using JetBrains.Annotations;
using Lykke.Job.QuorumExplorer.Settings;
using Lykke.SettingsReader;
using Nethereum.Web3;

namespace Lykke.Job.QuorumExplorer.Modules
{
    [UsedImplicitly]
    public class BlockchainModule : Module
    {
        private readonly BlockchainSettings _blockchainSettings;

        public BlockchainModule(
            IReloadingManager<AppSettings> appSettings)
        {
            _blockchainSettings = appSettings.CurrentValue.QuorumExplorerJob.Blockchain;
        }

        protected override void Load(
            ContainerBuilder builder)
        {
            builder
                .RegisterInstance(new Web3(_blockchainSettings.TransactionNodeUrl))
                .As<IWeb3>();
        }
    }
}
