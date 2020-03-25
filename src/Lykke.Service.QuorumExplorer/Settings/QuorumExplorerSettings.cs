using JetBrains.Annotations;

namespace Lykke.Service.QuorumExplorer.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class QuorumExplorerSettings
    {
        public DbSettings Db { get; set; }
    }
}
