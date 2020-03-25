using Autofac;
using JetBrains.Annotations;
using Lykke.Common.MsSql;
using Lykke.Job.QuorumExplorer.Settings;
using Lykke.Service.QuorumExplorer.Domain.Repositories;
using Lykke.Service.QuorumExplorer.MsSqlRepositories;
using Lykke.Service.QuorumExplorer.MsSqlRepositories.Contexts;
using Lykke.SettingsReader;

namespace Lykke.Job.QuorumExplorer.Modules
{
    [UsedImplicitly]
    public class RepositoriesModule : Module
    {
        private readonly DbSettings _dbSettings;

        public RepositoriesModule(
            IReloadingManager<AppSettings> appSettings)
        {
            _dbSettings = appSettings.CurrentValue.QuorumExplorerJob.Db;
        }

        protected override void Load(
            ContainerBuilder builder)
        {
            builder
                .RegisterMsSql(() => new QeContext(_dbSettings.DataConnString, false, _dbSettings.CommandTimeoutSeconds));

            builder
                .RegisterType<EventRepository>()
                .As<IEventRepository>()
                .SingleInstance();
            
            builder
                .RegisterType<FunctionCallRepository>()
                .As<IFunctionCallRepository>()
                .SingleInstance();
            
            builder
                .RegisterType<TransactionRepository>()
                .As<ITransactionRepository>()
                .SingleInstance();
            
            builder
                .RegisterType<ABIRepository>()
                .As<IABIRepository>()
                .SingleInstance();

            builder
                .RegisterType<IndexingStateRepository>()
                .As<IIndexingStateRepository>()
                .SingleInstance();
        }
    }
}
