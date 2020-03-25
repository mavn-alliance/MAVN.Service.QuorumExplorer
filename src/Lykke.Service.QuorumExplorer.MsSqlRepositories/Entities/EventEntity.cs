using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Lykke.Service.QuorumExplorer.MsSqlRepositories.Entities
{
    [Table("events")]
    public class EventEntity
    {
        // Columns

        [Column("event_name"), Required]
        public string EventName { get; set; }

        [Column("event_signature"), Required]
        public string EventSignature { get; set; }
        
        [Column("log_index"), Required]
        public long LogIndex { get; set; }
        
        [Column("parameters_json"), Required]
        public string ParametersJson { get; set; }

        [Column("transaction_hash"), Required]
        public string TransactionHash { get; set; }
        
        [Column("block_timestamp"), Required]
        public long BlockTimestamp { get; set; }

        // Relations
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public ICollection<EventParameterEntity> Parameters { get; set; }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        public TransactionLogEntity TransactionLog { get; set; }

        internal static void OnLykkeModelCreating(
            ModelBuilder modelBuilder)
        {
            // Key

            modelBuilder
                .Entity<EventEntity>()
                .HasKey(x => new { x.LogIndex, x.TransactionHash });
            
            // Indexes

            modelBuilder
                .Entity<EventEntity>()
                .HasIndex(x => x.EventName);
            
            modelBuilder
                .Entity<EventEntity>()
                .HasIndex(x => x.EventSignature);

            modelBuilder
                .Entity<EventEntity>()
                .HasIndex(x => x.TransactionHash);

            modelBuilder
                .Entity<EventEntity>()
                .HasIndex(x => new {x.LogIndex, x.TransactionHash});

            modelBuilder
                .Entity<EventEntity>()
                .HasIndex(x => x.BlockTimestamp)
                .IsUnique(false);

            modelBuilder
                .Entity<EventEntity>()
                .HasIndex(x => new {x.BlockTimestamp, x.LogIndex, x.TransactionHash});
            
            modelBuilder
                .Entity<EventEntity>()
                .HasIndex(x => new {x.BlockTimestamp, x.LogIndex, x.TransactionHash, x.EventName});

            // Relations
            
            modelBuilder
                .Entity<EventEntity>()
                .HasMany(x => x.Parameters)
                .WithOne(x => x.Event)
                .HasForeignKey(x => new { x.LogIndex, x.TransactionHash })
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
