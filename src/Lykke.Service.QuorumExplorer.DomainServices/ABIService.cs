using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.Service.QuorumExplorer.Domain;
using Lykke.Service.QuorumExplorer.Domain.DTOs;
using Lykke.Service.QuorumExplorer.Domain.Repositories;
using Lykke.Service.QuorumExplorer.Domain.Services;
using Nethereum.ABI.JsonDeserialisation;
using Newtonsoft.Json.Linq;

namespace Lykke.Service.QuorumExplorer.DomainServices
{
    public class ABIService : IABIService
    {
        private readonly ABIDeserialiser _abiDeserializer;
        private readonly IABIRepository _abiRepository;
        private readonly ILog _log;


        public ABIService(
            IABIRepository abiRepository,
            ILogFactory logFactory)
        {
            _abiDeserializer = new ABIDeserialiser();
            _abiRepository = abiRepository;
            _log = logFactory.CreateLog(this);
        }
        
        
        public async Task<ContractABIRegistrationResult> RegisterContractABIAsync(
            string contractABI)
        {
            using (_log.BeginScope($"{nameof(RegisterContractABIAsync)}-{Guid.NewGuid()}"))
            {
                #region Logging
                
                _log.Debug("Registering contract ABI.");
                
                #endregion

                try
                {
                    // Currently it's an easiest way to validate input.
                    _abiDeserializer.DeserialiseContract(contractABI);
                }
                catch (Exception e)
                {
                    #region Logging
                    
                    _log.Error("Failed to register a contract ABI. Provided data is not properly formatted.", e);
                    
                    #endregion

                    return new ContractABIRegistrationResult
                    {
                        Error = ContractABIRegistrationError.InvalidInputFormat
                    };
                }
                
                var abiJson = JArray.Parse(contractABI);

                foreach (var item in abiJson.Select(x => $"[{x}]"))
                {
                    var abiObject = _abiDeserializer.DeserialiseContract(item);

                    if (abiObject.Events.Length == 1)
                    {
                        var eventAbi = abiObject.Events[0];

                        await _abiRepository.SaveABIAsync(new ABI
                        {
                            Abi = item,
                            Name = eventAbi.Name,
                            Signature = $"0x{eventAbi.Sha3Signature}",
                            Type = ABIType.Event
                        });
                        
                        #region Logging

                        _log.Info
                        (
                            "Event ABI  has been registered.",
                            new { eventAbi.Name, Signature = eventAbi.Sha3Signature, Type = ABIType.Event }
                        );

                        #endregion
                    }
                    else if (abiObject.Functions.Length == 1)
                    {
                        var functionAbi = abiObject.Functions[0];

                        if (!functionAbi.Constant)
                        {
                            await _abiRepository.SaveABIAsync(new ABI
                            {
                                Abi = item,
                                Name = functionAbi.Name,
                                Signature = $"0x{functionAbi.Sha3Signature}",
                                Type = ABIType.Event
                            });
                            
                            #region Logging

                            _log.Info
                            (
                                "Function call ABI has been registered.",
                                new { functionAbi.Name, Signature = functionAbi.Sha3Signature, Type = ABIType.FunctionCall }
                            );

                            #endregion
                        }
                        else
                        {
                            #region Logging

                            _log.Debug
                            (
                                "Read-only function call ABI has been skipped.",
                                new { functionAbi.Name, Signature = functionAbi.Sha3Signature, Type = ABIType.FunctionCall }
                            );

                            #endregion
                        }
                    }
                }
                
                return new ContractABIRegistrationResult();
            }
        }
    }
}
