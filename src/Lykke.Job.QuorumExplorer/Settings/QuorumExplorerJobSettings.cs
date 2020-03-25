using JetBrains.Annotations;

namespace Lykke.Job.QuorumExplorer.Settings
{
    public class QuorumExplorerJobSettings
    {
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public BlockchainSettings Blockchain { get; set; }
        
        public DbSettings Db { get; set; }
    }
}
