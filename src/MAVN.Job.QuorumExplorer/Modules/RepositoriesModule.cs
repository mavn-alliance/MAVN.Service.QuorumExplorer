using System;
using System.Data.Common;
using Autofac;
using JetBrains.Annotations;
using MAVN.Common.MsSql;
using Lykke.Job.QuorumExplorer.Settings;
using MAVN.Service.QuorumExplorer.Domain.Repositories;
using MAVN.Service.QuorumExplorer.MsSqlRepositories;
using MAVN.Service.QuorumExplorer.MsSqlRepositories.Contexts;
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
                .RegisterMsSql(
                    _dbSettings.DataConnString,
                    connString => new QeContext(connString, false, _dbSettings.CommandTimeoutSeconds),
                    dbConn => new QeContext(dbConn));

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
