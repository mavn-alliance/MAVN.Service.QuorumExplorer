using System;
using System.Collections.Generic;
using System.Linq;
using MAVN.Service.QuorumExplorer.Domain.DTOs;
using MAVN.Service.QuorumExplorer.MsSqlRepositories.Entities;

namespace MAVN.Service.QuorumExplorer.MsSqlRepositories.Extensions
{
    public static class EventEntityExtensions
    {
        public static EventInfo ToEventInfo(this EventEntity src, List<EventParameterEntity> eventParameters = null)
        {
            if (src == null)
                throw new ArgumentNullException(nameof(src));

            return new EventInfo
            {
                EventName = src.EventName,
                EventSignature = src.EventSignature,
                LogIndex = src.LogIndex,
                Address = src.TransactionLog?.Address,
                BlockHash = src.TransactionLog?.Transaction?.BlockHash,
                BlockNumber = src.TransactionLog?.Transaction?.BlockNumber ?? default(long),
                TransactionHash = src.TransactionHash,
                TransactionIndex = src.TransactionLog?.Transaction?.TransactionIndex ?? default(long),
                Timestamp = src.TransactionLog?.Transaction?.BlockTimestamp ?? default(long),
                Parameters =
                    (eventParameters?.Select(p => p.ToDto()) ?? src.Parameters?.Select(p => p.ToDto()))?.ToList() ??
                    Enumerable.Empty<EventParameter>().ToList()
            };
        }
    }
}
