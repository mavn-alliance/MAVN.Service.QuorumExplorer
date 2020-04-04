using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.QuorumExplorer.MsSqlRepositories.Entities
{
    [Table("function_calls")]
    public class FunctionCallEntity
    {
        // Columns

        [Column("function_name"), Required]
        public string FunctionName { get; set; }

        [Column("function_signature"), Required]
        public string FunctionSignature { get; set; }
        
        [Column("parameters_json"), Required]
        public string ParametersJson { get; set; }

        [Column("transaction_hash"), Required]
        public string TransactionHash { get; set; }
        
        
        // Relations
        
        public ICollection<FunctionCallParameterEntity> Parameters { get; set; }
        
        public TransactionEntity Transaction { get; set; }


        internal static void OnLykkeModelCreating(
            ModelBuilder modelBuilder)
        {
            // Key

            modelBuilder
                .Entity<FunctionCallEntity>()
                .HasKey(x => x.TransactionHash);
            
            // Indexes
            modelBuilder
                .Entity<FunctionCallEntity>()
                .HasIndex(x => x.TransactionHash);
            
            // Relations

            modelBuilder
                .Entity<FunctionCallEntity>()
                .HasMany(x => x.Parameters)
                .WithOne(x => x.FunctionCall)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
