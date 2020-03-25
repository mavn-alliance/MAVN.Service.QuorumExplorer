using System;
using Lykke.Service.QuorumExplorer.Domain.DTOs;
using Lykke.Service.QuorumExplorer.MsSqlRepositories.Entities;

namespace Lykke.Service.QuorumExplorer.MsSqlRepositories.Extensions
{
    public static class TransactionEntityExtensions
    {
        public static TransactionInfo ToTransactionInfo(this TransactionEntity src)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));

            return new TransactionInfo
            {
                BlockHash = src.BlockHash,
                BlockNumber = src.BlockNumber,
                TransactionHash = src.TransactionHash,
                TransactionIndex = src.TransactionIndex,
                From = src.From,
                To = src.To,
                Status = src.Status,
                ContractAddress = src.ContractAddress,
                FunctionName = src.FunctionCall?.FunctionName,
                FunctionSignature = src.FunctionCall?.FunctionSignature,
                Timestamp = src.BlockTimestamp
            };
        }
    }
}
