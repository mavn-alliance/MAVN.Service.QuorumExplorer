using Autofac;
using JetBrains.Annotations;
using Lykke.Service.QuorumExplorer.Domain.Services;
using Lykke.Service.QuorumExplorer.Domain.Services.Explorer;
using Lykke.Service.QuorumExplorer.DomainServices;
using Lykke.Service.QuorumExplorer.DomainServices.Explorer;
using Lykke.Service.QuorumExplorer.Settings;
using Lykke.SettingsReader;

namespace Lykke.Service.QuorumExplorer.Modules
{
    [UsedImplicitly]
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public ServiceModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ABIService>()
                .As<IABIService>()
                .SingleInstance();

            builder.RegisterType<TransactionsReader>()
                .As<ITransactionsReader>();

            builder.RegisterType<EventsReader>()
                .As<IEventsReader>();
        }
    }
}
