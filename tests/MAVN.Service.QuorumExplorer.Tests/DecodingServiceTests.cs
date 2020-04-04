using System.Collections.Generic;
using MAVN.Service.QuorumExplorer.Domain.DTOs;
using MAVN.Service.QuorumExplorer.Domain.Services;
using MAVN.Service.QuorumExplorer.DomainServices;
using Xunit;

namespace MAVN.Service.QuorumExplorer.Tests
{
    public class DecodingServiceTests
    {
        private readonly IDecodingService _decodingService;

        
        public DecodingServiceTests()
        {
            _decodingService = new DecodingService();
        }

        
        [Theory]
        [MemberData(nameof(ValidTransactionInputs))]
        public void DecodeTransactionInput_ValidTransactionInput_NotRaiseException(
            TransactionInput transactionInput)
        {
            _decodingService.DecodeTransactionInput(transactionInput);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static IEnumerable<object[]> ValidTransactionInputs()
        {
            const string sendAbi = "[{\n  \"constant\": false,\n  \"inputs\": [\n    {\n      \"name\": \"recipient\",\n      \"type\": \"address\"\n    },\n    {\n      \"name\": \"amount\",\n      \"type\": \"uint256\"\n    },\n    {\n      \"name\": \"data\",\n      \"type\": \"bytes\"\n    }\n  ],\n  \"name\": \"send\",\n  \"outputs\": [],\n  \"payable\": false,\n  \"stateMutability\": \"nonpayable\",\n  \"type\": \"function\"\n}]";
            
            const string transactionHash = "0x1c5ada44376871595f49d0b2c5d6af61aa7279f10c516346d24fdcda7395d03e";
            
            yield return new object[]
            {
                new TransactionInput
                {
                    Abi = sendAbi,
                    EncodedInput = "0x9bd9bbc6000000000000000000000000afdea6bdb2f809ac2d5271b2b63befe628562e81000000000000000000000000000000000000000000000000000000000000000a000000000000000000000000000000000000000000000000000000000000006000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000",
                    TransactionHash = transactionHash
                }
            };
        }
    }
}
