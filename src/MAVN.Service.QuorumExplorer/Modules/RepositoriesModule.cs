using Autofac;
using JetBrains.Annotations;
using MAVN.Service.QuorumExplorer.Domain.Repositories;
using MAVN.Service.QuorumExplorer.MsSqlRepositories;
using MAVN.Service.QuorumExplorer.MsSqlRepositories.Contexts;
using MAVN.Service.QuorumExplorer.Settings;
using Lykke.SettingsReader;
using MAVN.Common.MsSql;

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
                .RegisterMsSql(
                    _dbSettings.DataConnString,
                    connString => new QeContext(connString, false, _dbSettings.CommandTimeoutSeconds),
                    dbConn => new QeContext(dbConn));

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
