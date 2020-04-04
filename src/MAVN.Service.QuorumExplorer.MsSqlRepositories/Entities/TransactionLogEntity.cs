// ReSharper disable SimilarAnonymousTypeNearby

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.QuorumExplorer.MsSqlRepositories.Entities
{
    [Table("transaction_logs")]
    public class TransactionLogEntity
    {
        // Columns

        [Column("address"), Required]
        public string Address { get; set; }

        [Column("data")]
        public string Data { get; set; }

        [Column("log_index"), Required]
        public long LogIndex { get; set; }

        [Column("topic_0"), Required]
        public string Topic0 { get; set; }

        [Column("topic_1")]
        public string Topic1 { get; set; }

        [Column("topic_2")]
        public string Topic2 { get; set; }

        [Column("topic_3")]
        public string Topic3 { get; set; }

        [Column("transaction_hash"), Required]
        public string TransactionHash { get; set; }
        
        [Column("decoded"), Required, DefaultValue(false)]
        public bool Decoded { get; set; }
        
        [Column("block_timestamp"), Required]
        public long BlockTimestamp { get; set; }

        
        // Relations
        
        public EventEntity Event { get; set; }
        
        public TransactionEntity Transaction { get; set; }
        
        
        internal static void OnLykkeModelCreating(
            ModelBuilder modelBuilder)
        {
            // Key

            modelBuilder
                .Entity<TransactionLogEntity>()
                .HasKey(x => new { x.LogIndex,x.TransactionHash });
            
            // Indexes

            modelBuilder
                .Entity<TransactionLogEntity>()
                .HasIndex(x => x.Address);
            
            modelBuilder
                .Entity<TransactionLogEntity>()
                .HasIndex(x => x.Topic0);
            
            modelBuilder
                .Entity<TransactionLogEntity>()
                .HasIndex(x => x.Topic1);
            
            modelBuilder
                .Entity<TransactionLogEntity>()
                .HasIndex(x => x.Topic2);
            
            modelBuilder
                .Entity<TransactionLogEntity>()
                .HasIndex(x => x.Topic3);

            modelBuilder
                .Entity<TransactionLogEntity>()
                .HasIndex(x => x.Decoded);

            modelBuilder
                .Entity<TransactionLogEntity>()
                .HasIndex(x => x.BlockTimestamp)
                .IsUnique(false);
            
            // Relations
            
            modelBuilder
                .Entity<TransactionLogEntity>()
                .HasOne(x => x.Event)
                .WithOne(x => x.TransactionLog)
                .HasForeignKey<EventEntity>(x => new { x.LogIndex, x.TransactionHash })
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
