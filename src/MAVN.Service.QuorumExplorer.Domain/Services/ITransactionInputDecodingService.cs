using System.Threading.Tasks;

namespace MAVN.Service.QuorumExplorer.Domain.Services
{
    public interface ITransactionInputDecodingService
    {
        Task<bool> DecodeNonDecodedInputsAsync(
            int batchSize);
    }
}
