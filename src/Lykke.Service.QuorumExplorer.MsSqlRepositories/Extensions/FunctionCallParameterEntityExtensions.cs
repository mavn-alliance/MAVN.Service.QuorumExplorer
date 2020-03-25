using System;
using Lykke.Service.QuorumExplorer.Domain.DTOs;
using Lykke.Service.QuorumExplorer.MsSqlRepositories.Entities;

namespace Lykke.Service.QuorumExplorer.MsSqlRepositories.Extensions
{
    public static class FunctionCallParameterEntityExtensions
    {
        public static FunctionCallParameter ToDto(this FunctionCallParameterEntity src)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));

            return new FunctionCallParameter
            {
                ParameterName = src.ParameterName,
                ParameterOrder = src.ParameterOrder,
                ParameterType = src.ParameterType,
                ParameterValue = src.ParameterValue
            };
        }
    }
}
