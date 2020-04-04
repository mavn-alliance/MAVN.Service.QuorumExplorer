using System;
using MAVN.Service.QuorumExplorer.Domain.DTOs;
using MAVN.Service.QuorumExplorer.MsSqlRepositories.Entities;

namespace MAVN.Service.QuorumExplorer.MsSqlRepositories.Extensions
{
    public static class AbiEntityExtensions
    {
        public static ABI ToDto(this ABIEntity src)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));
            
            return new ABI
            {
                Abi = src.Abi,
                Name = src.Name,
                Signature = src.Signature,
                Type = src.Type
            };
        }
    }
}
