using System;
using Lykke.Service.QuorumExplorer.Domain.DTOs;
using Lykke.Service.QuorumExplorer.MsSqlRepositories.Entities;

namespace Lykke.Service.QuorumExplorer.MsSqlRepositories.Extensions
{
    public static class EventParameterEntityExtensions
    {
        public static EventParameter ToDto(this EventParameterEntity src)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));

            return new EventParameter
            {
                ParameterName = src.ParameterName,
                ParameterType = src.ParameterType,
                ParameterOrder = src.ParameterOrder,
                ParameterValue = src.ParameterValue
            };
        }
    }
}
