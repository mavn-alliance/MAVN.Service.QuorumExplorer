using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using MAVN.Service.QuorumExplorer.Domain.DTOs;
using MAVN.Service.QuorumExplorer.Domain.Repositories;
using MAVN.Service.QuorumExplorer.Domain.Services;


namespace MAVN.Service.QuorumExplorer.DomainServices
{
    public class TransactionInputDecodingService : ITransactionInputDecodingService
    {
        private readonly IDecodingService _decodingService;
        private readonly IFunctionCallRepository _functionCallRepository;
        private readonly ILog _log;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionInputDecodingService(
            IDecodingService decodingService,
            IFunctionCallRepository functionCallRepository,
            ILogFactory logFactory,
            ITransactionRepository transactionRepository)
        {
            _decodingService = decodingService;
            _functionCallRepository = functionCallRepository;
            _log = logFactory.CreateLog(this);
            _transactionRepository = transactionRepository;
        }


        public async Task<bool> DecodeNonDecodedInputsAsync(
            int batchSize)
        {
            using (_log.BeginScope($"{nameof(DecodeNonDecodedInputsAsync)}-{Guid.NewGuid()}"))
            {
                IReadOnlyCollection<TransactionInput> nonDecodedTransactionInputs;

                try
                {
                    #region Logging
                    
                    _log.Debug($"Getting up to {batchSize} non-decoded transaction inputs with known ABIs.");
                    
                    #endregion

                    nonDecodedTransactionInputs = (await _transactionRepository.GetNonDecodedTransactionInputsAsync(batchSize)).ToList();

                    #region Logging
                    
                    _log.Debug($"Got {nonDecodedTransactionInputs.Count} non-decoded transaction inputs with known ABIs.");
                    
                    #endregion
                }
                catch (Exception e)
                {
                    #region Logging
                
                    _log.Error(e, $"Failed to get up to {batchSize} non-decoded transaction inputs with known ABIs.");
                
                    #endregion
                
                    return false;
                }

                if (nonDecodedTransactionInputs.Count == 0)
                {
                    #region Logging
                    
                    _log.Debug("No non-decoded transaction inputs with known ABIs have been found.");
                    
                    #endregion
                    
                    return false;
                }
                
                var decodedFunctionCalls = new List<FunctionCall>();

                foreach (var nonDecodedTransactionInput in nonDecodedTransactionInputs)
                {
                    try
                    {
                        #region Logging

                        _log.Debug
                        (
                            $"Decoding transaction [{nonDecodedTransactionInput.TransactionHash}] input.",
                            new {nonDecodedTransactionInput.TransactionHash}
                        );

                        #endregion

                        decodedFunctionCalls.Add(_decodingService.DecodeTransactionInput(nonDecodedTransactionInput));
                        
                        #region Logging

                        _log.Debug
                        (
                            $"Transaction [{nonDecodedTransactionInput.TransactionHash}] input has been decoded.",
                            new {nonDecodedTransactionInput.TransactionHash}
                        );

                        #endregion
                    }
                    catch (Exception e)
                    {
                        #region Logging

                        _log.Warning
                        (
                            $"Failed to decode transaction [{nonDecodedTransactionInput.TransactionHash}] input.",
                            e,
                            new { nonDecodedTransactionInput.TransactionHash, nonDecodedTransactionInput.Abi }
                        );

                        #endregion
                    }
                }

                try
                {
                    await _functionCallRepository.SaveFunctionCalls(decodedFunctionCalls);
                }
                catch (Exception e)
                {
                    #region Logging
                
                    _log.Error(e, "Failed to save decoded transaction inputs to DB");
                
                    #endregion
                    
                    return false;
                }
                
                #region Logging
                
                _log.Info($"{nonDecodedTransactionInputs.Count} transaction inputs have been decoded.");
                
                #endregion

                return true;
            }
        }
    }
}
