using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.QuorumExplorer.MsSqlRepositories.Entities
{
    [Table("transactions")]
    public class TransactionEntity
    {
        // Columns
        
        [Column("block_hash"), Required]
        public string BlockHash { get; set; }
        
        [Column("block_number"), Required]
        public long BlockNumber { get; set; }
        
        [Column("block_timestamp"), Required]
        public long BlockTimestamp { get; set; }

        [Column("contract_address")]
        public string ContractAddress { get; set; }

        [Column("from"), Required]
        public string From { get; set; }

        [Column("input")]
        public string Input { get; set; }
        
        [Column("input_signature")]
        public string InputSignature { get; set; }

        [Column("nonce"), Required]
        public long Nonce { get; set; }

        [Column("status"), Required]
        public long Status { get; set; }

        [Column("to")]
        public string To { get; set; }

        [Column("transaction_hash"), Required]
        public string TransactionHash { get; set; }

        [Column("transaction_index"), Required]
        public long TransactionIndex { get; set; }

        // Relations
        public FunctionCallEntity FunctionCall { get; set; }
        
        public ICollection<TransactionLogEntity> Logs { get; set; }
        
        internal static void OnLykkeModelCreating(
            ModelBuilder modelBuilder)
        {
            // Key

            modelBuilder
                .Entity<TransactionEntity>()
                .HasKey(x => x.TransactionHash);
            
            // Indexes
            
            modelBuilder
                .Entity<TransactionEntity>()
                .HasIndex(x => x.BlockHash);

            modelBuilder
                .Entity<TransactionEntity>()
                .HasIndex(x => x.BlockNumber);

            modelBuilder
                .Entity<TransactionEntity>()
                .HasIndex(x => new { x.BlockHash, x.TransactionIndex })
                .IsUnique();
            
            modelBuilder
                .Entity<TransactionEntity>()
                .HasIndex(x => x.ContractAddress);

            modelBuilder
                .Entity<TransactionEntity>()
                .HasIndex(x => x.From);

            modelBuilder
                .Entity<TransactionEntity>()
                .HasIndex(x => new { x.From, x.Nonce })
                .IsUnique();

            modelBuilder
                .Entity<TransactionEntity>()
                .HasIndex(x => x.Status);

            modelBuilder
                .Entity<TransactionEntity>()
                .HasIndex(x => x.To);

            modelBuilder
                .Entity<TransactionEntity>()
                .HasIndex(x => x.BlockTimestamp)
                .IsUnique(false);
            
            // Relations

            modelBuilder
                .Entity<TransactionEntity>()
                .HasOne(x => x.FunctionCall)
                .WithOne(x => x.Transaction)
                .HasForeignKey<FunctionCallEntity>(x => x.TransactionHash)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder
                .Entity<TransactionEntity>()
                .HasMany(x => x.Logs)
                .WithOne(x => x.Transaction)
                .HasForeignKey(x => x.TransactionHash)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
