using JetBrains.Annotations;

namespace Lykke.Job.QuorumExplorer.Settings
{
    public class BlockchainSettings
    {
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public string TransactionNodeUrl { get; set; }
    }
}
