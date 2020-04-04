using JetBrains.Annotations;
using Lykke.Sdk.Settings;

namespace MAVN.Service.QuorumExplorer.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public QuorumExplorerSettings QuorumExplorerService { get; set; }
    }
}
