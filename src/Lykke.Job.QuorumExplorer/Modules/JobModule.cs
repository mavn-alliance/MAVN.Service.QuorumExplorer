using Autofac;
using Lykke.Common.Log;
using Lykke.Common.MsSql;
using Lykke.Job.QuorumExplorer.Services;
using Lykke.Sdk;
using Lykke.Job.QuorumExplorer.Settings;
using Lykke.Service.QuorumExplorer.Domain.Services;
using Lykke.Service.QuorumExplorer.DomainServices;
using Lykke.SettingsReader;

namespace Lykke.Job.QuorumExplorer.Modules
{
    public class JobModule : Module
    {
        public JobModule(
            IReloadingManager<AppSettings> appSettings)
        {
            
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<BlockchainIndexingService>()
                .As<IBlockchainIndexingService>()
                .SingleInstance();

            builder
                .RegisterType<DecodingService>()
                .As<IDecodingService>()
                .SingleInstance();
            
            builder
                .RegisterType<TransactionInputDecodingService>()
                .As<ITransactionInputDecodingService>()
                .SingleInstance();
            
            builder
                .RegisterType<TransactionLogDecodingService>()
                .As<ITransactionLogDecodingService>()
                .SingleInstance();
            
            builder
                .Register(ctx => new BlockchainIndexingManager
                (
                    ctx.Resolve<IBlockchainIndexingService>(),
                    ctx.Resolve<ILogFactory>()
                ))
                .AsSelf()
                .SingleInstance();
            
            builder
                .Register(ctx => new TransactionInputDecodingManager
                (
                    100,
                    ctx.Resolve<ITransactionInputDecodingService>(),
                    ctx.Resolve<ILogFactory>()
                ))
                .AsSelf()
                .SingleInstance();
            
            builder
                .Register(ctx => new TransactionLogDecodingManager
                (
                    100,
                    ctx.Resolve<ITransactionLogDecodingService>(),
                    ctx.Resolve<ILogFactory>()
                ))
                .AsSelf()
                .SingleInstance();
            
            builder
                .Register(ctx => new StartupManager
                (
                    ctx.Resolve<BlockchainIndexingManager>(),
                    ctx.Resolve<ILogFactory>(),
                    ctx.Resolve<TransactionInputDecodingManager>(),
                    ctx.Resolve<TransactionLogDecodingManager>()
                ))
                .As<IStartupManager>()
                .SingleInstance();

            builder
                .Register(ctx => new ShutdownManager
                (
                    ctx.Resolve<BlockchainIndexingManager>(),
                    ctx.Resolve<ILogFactory>(),
                    ctx.Resolve<TransactionInputDecodingManager>(),
                    ctx.Resolve<TransactionLogDecodingManager>()
                ))
                .As<IShutdownManager>()
                .SingleInstance();
            
            builder
                .RegisterType<TransactionScopeHandler>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
