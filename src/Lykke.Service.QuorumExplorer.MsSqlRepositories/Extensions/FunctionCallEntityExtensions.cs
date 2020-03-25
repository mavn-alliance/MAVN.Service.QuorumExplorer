using System;
using System.Linq;
using Lykke.Service.QuorumExplorer.Domain.DTOs;
using Lykke.Service.QuorumExplorer.MsSqlRepositories.Entities;

namespace Lykke.Service.QuorumExplorer.MsSqlRepositories.Extensions
{
    public static class FunctionCallEntityExtensions
    {
        public static FunctionCall ToDto(this FunctionCallEntity src)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));

            return new FunctionCall
            {
                FunctionName = src.FunctionName,
                FunctionSignature = src.FunctionSignature,
                ParametersJson = src.ParametersJson,
                TransactionHash = src.TransactionHash,
                Parameters = src.Parameters?.Select(p => p.ToDto()).ToList() ??
                             Enumerable.Empty<FunctionCallParameter>().ToList()
            };
        }
    }
}
