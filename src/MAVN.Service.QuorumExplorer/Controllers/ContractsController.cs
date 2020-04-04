using System.Threading.Tasks;
using MAVN.Service.QuorumExplorer.Client;
using MAVN.Service.QuorumExplorer.Client.Models;
using MAVN.Service.QuorumExplorer.Domain.DTOs;
using MAVN.Service.QuorumExplorer.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace MAVN.Service.QuorumExplorer.Controllers
{
    [ApiController]
    [Route("api/contracts")]
    public class ContractsController : ControllerBase, IContractsApi
    {
        private readonly IABIService _abiService;

        public ContractsController(
            IABIService abiService)
        {
            _abiService = abiService;
        }

        [HttpPost]
        public async Task<RegisterABIResponse> RegisterAbiAsync([FromBody] RegisterABIRequest request)
        {
            var registrationResult = await _abiService.RegisterContractABIAsync(request.Abi);
            var response = new RegisterABIResponse();

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (registrationResult.Error)
            {
                case ContractABIRegistrationError.InvalidInputFormat:
                    response.Error = RegisterABIError.InvalidInputFormat;
                    break;
            }

            return response;
        }
    }
}
