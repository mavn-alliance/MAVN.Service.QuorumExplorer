using Autofac;
using JetBrains.Annotations;
using MAVN.Service.QuorumExplorer.Domain.Services;
using MAVN.Service.QuorumExplorer.Domain.Services.Explorer;
using MAVN.Service.QuorumExplorer.DomainServices;
using MAVN.Service.QuorumExplorer.DomainServices.Explorer;
using MAVN.Service.QuorumExplorer.Settings;
using Lykke.SettingsReader;

namespace MAVN.Service.QuorumExplorer.Modules
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
