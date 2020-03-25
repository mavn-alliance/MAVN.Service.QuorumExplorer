using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lykke.Service.QuorumExplorer.MsSqlRepositories.Entities
{
    [Table("event_parameters")]
    public class EventParameterEntity
    {
        // Columns

        [Column("log_index"), Required]
        public long LogIndex { get; set; }

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
        
        public EventEntity Event { get; set; }
        
        
        internal static void OnLykkeModelCreating(
            ModelBuilder modelBuilder)
        {
            // Keys

            modelBuilder
                .Entity<EventParameterEntity>()
                .HasKey(x => new { x.LogIndex, x.ParameterOrder, x.TransactionHash });
            
            // Indexes

            modelBuilder
                .Entity<EventParameterEntity>()
                .HasIndex(x => new { x.ParameterType, x.ParameterValueHash });

            modelBuilder
                .Entity<EventParameterEntity>()
                .HasIndex(x => x.TransactionHash);
        }
    }
}
