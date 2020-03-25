using System.Threading.Tasks;
using Lykke.Service.QuorumExplorer.Domain.DTOs;

namespace Lykke.Service.QuorumExplorer.Domain.Services
{
    public interface IABIService
    {
        Task<ContractABIRegistrationResult> RegisterContractABIAsync(
            string contractABI);
    }
}
