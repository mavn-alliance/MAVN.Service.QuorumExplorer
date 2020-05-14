using JetBrains.Annotations;
using MAVN.Common.MsSql;
using MAVN.Service.QuorumExplorer.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MAVN.Service.QuorumExplorer.MsSqlRepositories.Contexts
{
    public class QeContext : MsSqlContext
    {
        private const string Schema = "quorum_explorer";

        
        public QeContext()
            : base(Schema)
        {
        }

        public QeContext(
            string connectionString,
            bool isTraceEnabled,
            int commandTimeoutSeconds)
            : base(Schema, connectionString, isTraceEnabled, commandTimeoutSeconds)
        {
        }

        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        internal DbSet<ABIEntity> ABIs { get; set; }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        internal DbSet<EventEntity> Events { get; set; }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        internal DbSet<EventParameterEntity> EventParameters { get; set; }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        internal DbSet<FunctionCallEntity> FunctionCalls { get; set; }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        internal DbSet<FunctionCallParameterEntity> FunctionCallParameters { get; set; }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        internal DbSet<TransactionEntity> Transactions { get; set; }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        internal DbSet<TransactionLogEntity> TransactionLogs { get; set; }
        
        [UsedImplicitly(ImplicitUseKindFlags.Assign)]
        internal DbSet<BlocksDataEntity> BlocksData { get; set; }


        protected override void OnLykkeConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Log(RelationalEventId.QueryClientEvaluationWarning));
        }

        protected override void OnLykkeModelCreating(
            ModelBuilder modelBuilder)
        {
            ABIEntity.OnLykkeModelCreating(modelBuilder);
            EventEntity.OnLykkeModelCreating(modelBuilder);
            EventParameterEntity.OnLykkeModelCreating(modelBuilder);
            FunctionCallEntity.OnLykkeModelCreating(modelBuilder);
            FunctionCallParameterEntity.OnLykkeModelCreating(modelBuilder);
            TransactionEntity.OnLykkeModelCreating(modelBuilder);
            TransactionLogEntity.OnLykkeModelCreating(modelBuilder);
        }
    }
}
