using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.QuorumExplorer.Domain.DTOs;

namespace Lykke.Service.QuorumExplorer.Domain.Repositories
{
    public interface IABIRepository
    {
        Task SaveABIAsync(ABI abi);

        Task<IEnumerable<ABI>> GetAsync();
    }
}
