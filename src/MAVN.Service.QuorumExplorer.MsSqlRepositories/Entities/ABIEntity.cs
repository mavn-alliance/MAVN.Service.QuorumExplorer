using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Service.QuorumExplorer.Domain;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.QuorumExplorer.MsSqlRepositories.Entities
{
    [Table("ABIs")]
    public class ABIEntity
    {
        [Column("abi"), Required]
        public string Abi { get; set; }
        
        [Column("name"), Required]
        public string Name { get; set; }
        
        [Column("signature"), Required]
        public string Signature { get; set; }
        
        [Column("type"), Required]
        public ABIType Type { get; set; }
        
        
        internal static void OnLykkeModelCreating(
            ModelBuilder modelBuilder)
        {
            // Key

            modelBuilder
                .Entity<ABIEntity>()
                .HasKey(x => x.Signature);
            
            // Indexes
            
            modelBuilder
                .Entity<ABIEntity>()
                .HasIndex(x => x.Name);
            
            modelBuilder
                .Entity<ABIEntity>()
                .HasIndex(x => x.Type);
        }
    }
}
