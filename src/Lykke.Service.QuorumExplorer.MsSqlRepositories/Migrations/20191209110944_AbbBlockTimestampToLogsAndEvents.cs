using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.QuorumExplorer.MsSqlRepositories.Migrations
{
    public partial class AbbBlockTimestampToLogsAndEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "block_timestamp",
                schema: "quorum_explorer",
                table: "transaction_logs",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "block_timestamp",
                schema: "quorum_explorer",
                table: "events",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.Sql(@"
            update tl
            set tl.block_timestamp = t.block_timestamp
            from quorum_explorer.transaction_logs tl
                     inner join quorum_explorer.transactions t
                                on t.transaction_hash = tl.transaction_hash
            ");

            migrationBuilder.Sql(@"
            update e
            set e.block_timestamp = tl.block_timestamp
            from quorum_explorer.events e
                     inner join quorum_explorer.transaction_logs tl
                                on (e.log_index = tl.log_index) AND (e.transaction_hash = tl.transaction_hash)
            ");
            
            migrationBuilder.CreateIndex(
                name: "IX_transactions_block_timestamp",
                schema: "quorum_explorer",
                table: "transactions",
                column: "block_timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_logs_block_timestamp",
                schema: "quorum_explorer",
                table: "transaction_logs",
                column: "block_timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_events_block_timestamp",
                schema: "quorum_explorer",
                table: "events",
                column: "block_timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_transactions_block_timestamp",
                schema: "quorum_explorer",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "IX_transaction_logs_block_timestamp",
                schema: "quorum_explorer",
                table: "transaction_logs");

            migrationBuilder.DropIndex(
                name: "IX_events_block_timestamp",
                schema: "quorum_explorer",
                table: "events");

            migrationBuilder.DropColumn(
                name: "block_timestamp",
                schema: "quorum_explorer",
                table: "transaction_logs");

            migrationBuilder.DropColumn(
                name: "block_timestamp",
                schema: "quorum_explorer",
                table: "events");
        }
    }
}
