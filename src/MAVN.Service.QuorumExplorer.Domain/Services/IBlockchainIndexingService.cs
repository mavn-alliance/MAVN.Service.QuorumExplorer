using System.Threading.Tasks;

namespace MAVN.Service.QuorumExplorer.Domain.Services
{
    public interface IBlockchainIndexingService
    {
        Task<bool> IndexNonIndexedBlocksAsync();
    }
}
