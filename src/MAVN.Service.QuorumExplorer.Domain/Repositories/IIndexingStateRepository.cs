using System.Threading.Tasks;

namespace MAVN.Service.QuorumExplorer.Domain.Repositories
{
    public interface IIndexingStateRepository
    {
        Task<long?> GetLastIndexedBlockNumberAsync();

        Task SaveLastIndexedBlockNumber(long blockNumber);
    }
}
