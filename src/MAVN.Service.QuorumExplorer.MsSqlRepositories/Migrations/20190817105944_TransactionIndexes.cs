using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.QuorumExplorer.MsSqlRepositories.Migrations
{
    public partial class TransactionIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_function_calls_transaction_hash",
                schema: "quorum_explorer",
                table: "function_calls",
                column: "transaction_hash",
                unique: true);
            
            migrationBuilder.Sql(
                @"CREATE INDEX IX_transactions_transaction_hash on [quorum_explorer].[transactions] (transaction_hash) INCLUDE(input_signature, input)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_function_calls_transaction_hash",
                schema: "quorum_explorer",
                table: "function_calls");

            migrationBuilder.DropIndex(
                "IX_transactions_transaction_hash",
                schema: "quorum_explorer",
                table: "transactions");
        }
    }
}
