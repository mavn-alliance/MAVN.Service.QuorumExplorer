using System.Threading.Tasks;

namespace Lykke.Service.QuorumExplorer.Domain.Services
{
    public interface ITransactionInputDecodingService
    {
        Task<bool> DecodeNonDecodedInputsAsync(
            int batchSize);
    }
}
