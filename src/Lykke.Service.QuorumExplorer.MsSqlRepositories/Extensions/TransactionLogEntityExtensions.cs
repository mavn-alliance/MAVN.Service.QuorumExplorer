using System;
using System.Collections.Generic;
using System.Linq;
using Lykke.Service.QuorumExplorer.Domain.DTOs;
using Lykke.Service.QuorumExplorer.MsSqlRepositories.Entities;

namespace Lykke.Service.QuorumExplorer.MsSqlRepositories.Extensions
{
    internal static class TransactionLogEntityExtensions
    {
        public static IEnumerable<string> GetTopics(
            this TransactionLogEntity entity)
        {
            yield return entity.Topic0;

            if (entity.Topic1 != null)
            {
                yield return entity.Topic1;
            }
            else
            {
                yield break;
            }
            
            if (entity.Topic2 != null)
            {
                yield return entity.Topic2;
            }
            else
            {
                yield break;
            }
            
            if (entity.Topic3 != null)
            {
                yield return entity.Topic3;
            }
        }

        public static TransactionLog ToDto(this TransactionLogEntity src)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));

            return new TransactionLog
            {
                Address = src.Address,
                LogIndex = src.LogIndex,
                TransactionHash = src.TransactionHash,
                Topics = src.GetTopics().ToArray(),
                BlockTimestamp = src.BlockTimestamp
            };
        }
    }
}
