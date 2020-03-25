using JetBrains.Annotations;
using Lykke.Sdk.Settings;

namespace Lykke.Service.QuorumExplorer.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public QuorumExplorerSettings QuorumExplorerService { get; set; }
    }
}
