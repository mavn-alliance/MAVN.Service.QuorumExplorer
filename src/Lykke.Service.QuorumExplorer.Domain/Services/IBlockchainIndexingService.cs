using System.Threading.Tasks;

namespace Lykke.Service.QuorumExplorer.Domain.Services
{
    public interface IBlockchainIndexingService
    {
        Task<bool> IndexNonIndexedBlocksAsync();
    }
}
