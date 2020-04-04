using System;
using MAVN.Service.QuorumExplorer.Domain.DTOs;
using MAVN.Service.QuorumExplorer.MsSqlRepositories.Entities;

namespace MAVN.Service.QuorumExplorer.MsSqlRepositories.Extensions
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
