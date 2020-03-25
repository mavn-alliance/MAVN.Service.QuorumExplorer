using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.QuorumExplorer.Client.Models;
using Refit;

namespace Lykke.Service.QuorumExplorer.Client
{
    /// <summary>
    /// Contracts API interface
    /// </summary>
    [PublicAPI]
    public interface IContractsApi
    {
        /// <summary>
        /// Register new ABI
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Post("/api/contracts")]
        Task<RegisterABIResponse> RegisterAbiAsync([Body] RegisterABIRequest request);
    }
}
