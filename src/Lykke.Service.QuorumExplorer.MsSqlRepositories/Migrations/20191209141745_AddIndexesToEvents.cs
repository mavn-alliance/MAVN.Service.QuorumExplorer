using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.QuorumExplorer.MsSqlRepositories.Migrations
{
    public partial class AddIndexesToEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            create index IX_events_block_timestamp_log_index_transaction_hash
	            on quorum_explorer.events (block_timestamp desc, log_index asc, transaction_hash asc)
            ");
            
            migrationBuilder.Sql(@"
            create index IX_events_block_timestamp_log_index_transaction_hash_event_name
	            on quorum_explorer.events (block_timestamp desc, log_index asc, transaction_hash asc, event_name asc)
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_events_block_timestamp_log_index_transaction_hash",
                schema: "quorum_explorer",
                table: "events");

            migrationBuilder.DropIndex(
                name: "IX_events_block_timestamp_log_index_transaction_hash_event_name",
                schema: "quorum_explorer",
                table: "events");
        }
    }
}
