using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.QuorumExplorer.MsSqlRepositories.Migrations
{
    public partial class TransactionLogIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_events_log_index_transaction_hash",
                schema: "quorum_explorer",
                table: "events",
                columns: new[] { "log_index", "transaction_hash" },
                unique: true);

            migrationBuilder.Sql(
                @"CREATE INDEX IX_transaction_logs_logIndex_transaction_hash on [quorum_explorer].[transaction_logs] (log_index, transaction_hash) INCLUDE(address, data, topic_0, topic_1, topic_2, topic_3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_events_log_index_transaction_hash",
                schema: "quorum_explorer",
                table: "events");

            migrationBuilder.DropIndex(
                "IX_transaction_logs_logIndex_transaction_hash",
                schema: "quorum_explorer",
                table: "transaction_logs");
        }
    }
}
