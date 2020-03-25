using System.Threading.Tasks;

namespace Lykke.Service.QuorumExplorer.Domain.Services
{
    public interface ITransactionLogDecodingService
    {
        Task<bool> DecodeNonDecodedTopicsAsync(
            int batchSize);
    }
}
