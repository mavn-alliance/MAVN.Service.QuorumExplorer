using System.Threading.Tasks;

namespace MAVN.Service.QuorumExplorer.Domain.Services
{
    public interface ITransactionLogDecodingService
    {
        Task<bool> DecodeNonDecodedTopicsAsync(
            int batchSize);
    }
}
