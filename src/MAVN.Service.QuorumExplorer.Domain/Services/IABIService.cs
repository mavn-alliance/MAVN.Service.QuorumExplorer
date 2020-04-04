using System.Threading.Tasks;
using MAVN.Service.QuorumExplorer.Domain.DTOs;

namespace MAVN.Service.QuorumExplorer.Domain.Services
{
    public interface IABIService
    {
        Task<ContractABIRegistrationResult> RegisterContractABIAsync(
            string contractABI);
    }
}
