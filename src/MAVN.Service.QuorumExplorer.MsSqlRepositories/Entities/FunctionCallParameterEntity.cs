using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.QuorumExplorer.MsSqlRepositories.Entities
{
    [Table("function_call_parameters")]
    public class FunctionCallParameterEntity
    {
        // Columns

        [Column("parameter_name"), Required]
        public string ParameterName { get; set; }

        [Column("parameter_order"), Required]
        public int ParameterOrder { get; set; }

        [Column("parameter_type"), Required]
        public string ParameterType { get; set; }

        [Column("parameter_value"), Required]
        public string ParameterValue { get; set; }
        
        [Column("parameter_value_hash"), Required]
        public string ParameterValueHash { get; set; }

        [Column("transaction_hash"), Required]
        public string TransactionHash { get; set; }

        
        // Relations
        
        public FunctionCallEntity FunctionCall { get; set; }
        
        
        internal static void OnLykkeModelCreating(
            ModelBuilder modelBuilder)
        {
            // Key

            modelBuilder
                .Entity<FunctionCallParameterEntity>()
                .HasKey(x => new { x.ParameterOrder, x.TransactionHash });
            
            // Indexes
            
            modelBuilder
                .Entity<FunctionCallParameterEntity>()
                .HasIndex(x => new { x.ParameterType, x.ParameterValueHash });
        }
    }
}
