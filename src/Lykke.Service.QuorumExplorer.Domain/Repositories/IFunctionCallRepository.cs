using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.QuorumExplorer.Domain.DTOs;

namespace Lykke.Service.QuorumExplorer.Domain.Repositories
{
    public interface IFunctionCallRepository
    {
        Task SaveFunctionCalls(
            IReadOnlyCollection<FunctionCall> functionCalls);
    }
}
