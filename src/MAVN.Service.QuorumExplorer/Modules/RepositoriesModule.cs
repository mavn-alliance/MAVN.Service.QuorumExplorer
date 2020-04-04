using Autofac;
using JetBrains.Annotations;
using Lykke.Common.MsSql;
using MAVN.Service.QuorumExplorer.Domain.Repositories;
using MAVN.Service.QuorumExplorer.MsSqlRepositories;
using MAVN.Service.QuorumExplorer.MsSqlRepositories.Contexts;
using MAVN.Service.QuorumExplorer.Settings;
using Lykke.SettingsReader;

namespace MAVN.Service.QuorumExplorer.Modules
{
    [UsedImplicitly]
    public class RepositoriesModule : Module
    {
        private readonly DbSettings _dbSettings;

        public RepositoriesModule(
            IReloadingManager<AppSettings> appSettings)
        {
            _dbSettings = appSettings.CurrentValue.QuorumExplorerService.Db;
        }

        protected override void Load(
            ContainerBuilder builder)
        {
            builder
                .RegisterMsSql(() => new QeContext(_dbSettings.DataConnString, false, _dbSettings.CommandTimeoutSeconds));

            builder
                .RegisterType<ABIRepository>()
                .As<IABIRepository>()
                .SingleInstance();

            builder
                .RegisterType<TransactionRepository>()
                .As<ITransactionRepository>()
                .SingleInstance();
            
            builder
                .RegisterType<EventRepository>()
                .As<IEventRepository>()
                .SingleInstance();
        }
    }
}
