using System.Threading.Tasks;

namespace Lykke.Service.QuorumExplorer.Domain.Repositories
{
    public interface IIndexingStateRepository
    {
        Task<long?> GetLastIndexedBlockNumberAsync();

        Task SaveLastIndexedBlockNumber(long blockNumber);
    }
}
