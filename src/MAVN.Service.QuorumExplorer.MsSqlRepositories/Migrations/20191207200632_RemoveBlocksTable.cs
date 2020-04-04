using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.QuorumExplorer.MsSqlRepositories.Migrations
{
    public partial class RemoveBlocksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "block_timestamp",
                schema: "quorum_explorer",
                table: "transactions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.Sql(@"
            update t
            set t.block_timestamp = b.timestamp
            from quorum_explorer.transactions t
                     inner join quorum_explorer.blocks b
                                on t.block_hash = b.block_hash
            ");
            
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_blocks_block_hash",
                schema: "quorum_explorer",
                table: "transactions");

            migrationBuilder.CreateTable(
                name: "blocks_data",
                schema: "quorum_explorer",
                columns: table => new
                {
                    key = table.Column<string>(nullable: false),
                    value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blocks_data", x => x.key);
                });

            migrationBuilder.Sql(@"
            insert into quorum_explorer.blocks_data ([key], [value])
            select 'LastIndexedBlockNumber', case when max(number) is null then 0 else max(number) end
            from quorum_explorer.blocks
            ");

            migrationBuilder.DropTable(
                name: "blocks",
                schema: "quorum_explorer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blocks_data",
                schema: "quorum_explorer");

            migrationBuilder.DropColumn(
                name: "block_timestamp",
                schema: "quorum_explorer",
                table: "transactions");

            migrationBuilder.CreateTable(
                name: "blocks",
                schema: "quorum_explorer",
                columns: table => new
                {
                    block_hash = table.Column<string>(nullable: false),
                    number = table.Column<long>(nullable: false),
                    parent_hash = table.Column<string>(nullable: true),
                    timestamp = table.Column<long>(nullable: false),
                    transactions_count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blocks", x => x.block_hash);
                    table.ForeignKey(
                        name: "FK_blocks_blocks_parent_hash",
                        column: x => x.parent_hash,
                        principalSchema: "quorum_explorer",
                        principalTable: "blocks",
                        principalColumn: "block_hash",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_blocks_number",
                schema: "quorum_explorer",
                table: "blocks",
                column: "number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_blocks_parent_hash",
                schema: "quorum_explorer",
                table: "blocks",
                column: "parent_hash",
                unique: true,
                filter: "[parent_hash] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_blocks_timestamp",
                schema: "quorum_explorer",
                table: "blocks",
                column: "timestamp");

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_blocks_block_hash",
                schema: "quorum_explorer",
                table: "transactions",
                column: "block_hash",
                principalSchema: "quorum_explorer",
                principalTable: "blocks",
                principalColumn: "block_hash",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
