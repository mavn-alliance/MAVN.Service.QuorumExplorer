using JetBrains.Annotations;

namespace MAVN.Service.QuorumExplorer.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class QuorumExplorerSettings
    {
        public DbSettings Db { get; set; }
    }
}
