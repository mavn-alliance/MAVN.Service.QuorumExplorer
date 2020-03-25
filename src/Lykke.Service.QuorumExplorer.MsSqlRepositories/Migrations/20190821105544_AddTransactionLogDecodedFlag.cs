using Microsoft.EntityFrameworkCore.Migrations;

namespace Lykke.Service.QuorumExplorer.MsSqlRepositories.Migrations
{
    public partial class AddTransactionLogDecodedFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "decoded",
                schema: "quorum_explorer",
                table: "transaction_logs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_transaction_logs_decoded",
                schema: "quorum_explorer",
                table: "transaction_logs",
                column: "decoded");

            migrationBuilder.Sql(@"
                UPDATE [o]
                SET [o].[decoded] = 1
                FROM [quorum_explorer].[transaction_logs] AS [o]
                         LEFT JOIN [quorum_explorer].[events] AS [o.Event] ON ([o].[log_index] = [o.Event].[log_index]) AND
                                                                              ([o].[transaction_hash] = [o.Event].[transaction_hash])
                WHERE [o.Event].[log_index] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_transaction_logs_decoded",
                schema: "quorum_explorer",
                table: "transaction_logs");

            migrationBuilder.DropColumn(
                name: "decoded",
                schema: "quorum_explorer",
                table: "transaction_logs");
        }
    }
}
