using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.QuorumExplorer.Domain.DTOs;

namespace MAVN.Service.QuorumExplorer.Domain.Repositories
{
    public interface IABIRepository
    {
        Task SaveABIAsync(ABI abi);

        Task<IEnumerable<ABI>> GetAsync();
    }
}
