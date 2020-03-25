using System.Collections.Generic;
using System.Linq;
using Lykke.Service.QuorumExplorer.Domain.DTOs;
using Lykke.Service.QuorumExplorer.Domain.Services;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.ABI.JsonDeserialisation;
using Nethereum.Hex.HexConvertors.Extensions;
using Newtonsoft.Json.Linq;

namespace Lykke.Service.QuorumExplorer.DomainServices
{
    public class DecodingService : IDecodingService
    {
        private readonly ABIDeserialiser _abiDeserializer;
        private readonly EventTopicDecoder _eventTopicDecoder;
        private readonly FunctionCallDecoder _functionCallDecoder;


        public DecodingService()
        {
            _abiDeserializer = new ABIDeserialiser();
            _eventTopicDecoder = new EventTopicDecoder();
            _functionCallDecoder = new FunctionCallDecoder();
        }

        public Event DecodeTransactionLog(
            TransactionLog transactionLog)
        {
            var abiObject = _abiDeserializer.DeserialiseContract(transactionLog.Abi).Events[0];
            // ReSharper disable once CoVariantArrayConversion
            var parameters = _eventTopicDecoder.DecodeDefaultTopics(abiObject, transactionLog.Topics, transactionLog.Data);

            return new Event
            {
                EventName = abiObject.Name,
                EventSignature = $"0x{abiObject.Sha3Signature}",
                LogIndex = transactionLog.LogIndex,
                Parameters = parameters.Select(x => new EventParameter
                {
                    ParameterName = x.Parameter.Name,
                    ParameterOrder = x.Parameter.Order,
                    ParameterType = x.Parameter.Type,
                    ParameterValue = StringifyParameter(x)
                }).ToList(),
                ParametersJson = SerializeParameters(parameters),
                TransactionHash = transactionLog.TransactionHash,
                BlockTimestamp = transactionLog.BlockTimestamp
            };
        }

        public FunctionCall DecodeTransactionInput(
            TransactionInput transactionInput)
        {
            var abiObject = _abiDeserializer.DeserialiseContract(transactionInput.Abi).Functions[0];
            var parameters = _functionCallDecoder.DecodeFunctionInput
            (
                abiObject.Sha3Signature, 
                transactionInput.EncodedInput,
                abiObject.InputParameters
            );
            
            return new FunctionCall
            {
                FunctionName = abiObject.Name,
                FunctionSignature = $"0x{abiObject.Sha3Signature}",
                Parameters = parameters.Select(x => new FunctionCallParameter
                {
                    ParameterName = x.Parameter.Name,
                    ParameterOrder = x.Parameter.Order,
                    ParameterType = x.Parameter.Type,
                    ParameterValue = StringifyParameter(x)
                }).ToList(),
                ParametersJson = SerializeParameters(parameters),
                TransactionHash = transactionInput.TransactionHash
            };
        }

        private static string SerializeParameters(
            IEnumerable<ParameterOutput> parameters)
        {
            var result = new JObject();

            foreach (var parameter in parameters)
            {
                object parameterValue;
                
                switch (parameter.Result)
                {
                    case byte[] br:
                        parameterValue = br.ToHex(true);
                        break;
                    default:
                        parameterValue = parameter.Result;
                        break;
                }
                
                result.Add(parameter.Parameter.Name, new JValue(parameterValue));
            }

            return result.ToString();
        }

        private static string StringifyParameter(
            ParameterOutput parameter)
        {
            switch (parameter.Result)
            {
                case byte[] br:
                    return br.ToHex(true);
                default:
                    return parameter.Result.ToString();
            }
        }
    }
}
