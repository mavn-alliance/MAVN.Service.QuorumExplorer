using Microsoft.EntityFrameworkCore.Migrations;

namespace MAVN.Service.QuorumExplorer.MsSqlRepositories.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "quorum_explorer");

            migrationBuilder.CreateTable(
                name: "ABIs",
                schema: "quorum_explorer",
                columns: table => new
                {
                    signature = table.Column<string>(nullable: false),
                    abi = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ABIs", x => x.signature);
                });

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

            migrationBuilder.CreateTable(
                name: "transactions",
                schema: "quorum_explorer",
                columns: table => new
                {
                    transaction_hash = table.Column<string>(nullable: false),
                    block_hash = table.Column<string>(nullable: false),
                    block_number = table.Column<long>(nullable: false),
                    block_timestamp = table.Column<long>(nullable: false),
                    contract_address = table.Column<string>(nullable: true),
                    from = table.Column<string>(nullable: false),
                    input = table.Column<string>(nullable: true),
                    input_signature = table.Column<string>(nullable: true),
                    nonce = table.Column<long>(nullable: false),
                    status = table.Column<long>(nullable: false),
                    to = table.Column<string>(nullable: true),
                    transaction_index = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.transaction_hash);
                });

            migrationBuilder.CreateTable(
                name: "function_calls",
                schema: "quorum_explorer",
                columns: table => new
                {
                    transaction_hash = table.Column<string>(nullable: false),
                    function_name = table.Column<string>(nullable: false),
                    function_signature = table.Column<string>(nullable: false),
                    parameters_json = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_function_calls", x => x.transaction_hash);
                    table.ForeignKey(
                        name: "FK_function_calls_transactions_transaction_hash",
                        column: x => x.transaction_hash,
                        principalSchema: "quorum_explorer",
                        principalTable: "transactions",
                        principalColumn: "transaction_hash",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "transaction_logs",
                schema: "quorum_explorer",
                columns: table => new
                {
                    log_index = table.Column<long>(nullable: false),
                    transaction_hash = table.Column<string>(nullable: false),
                    address = table.Column<string>(nullable: false),
                    data = table.Column<string>(nullable: true),
                    topic_0 = table.Column<string>(nullable: false),
                    topic_1 = table.Column<string>(nullable: true),
                    topic_2 = table.Column<string>(nullable: true),
                    topic_3 = table.Column<string>(nullable: true),
                    decoded = table.Column<bool>(nullable: false),
                    block_timestamp = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction_logs", x => new { x.log_index, x.transaction_hash });
                    table.ForeignKey(
                        name: "FK_transaction_logs_transactions_transaction_hash",
                        column: x => x.transaction_hash,
                        principalSchema: "quorum_explorer",
                        principalTable: "transactions",
                        principalColumn: "transaction_hash",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "function_call_parameters",
                schema: "quorum_explorer",
                columns: table => new
                {
                    parameter_order = table.Column<int>(nullable: false),
                    transaction_hash = table.Column<string>(nullable: false),
                    parameter_name = table.Column<string>(nullable: false),
                    parameter_type = table.Column<string>(nullable: false),
                    parameter_value = table.Column<string>(nullable: false),
                    parameter_value_hash = table.Column<string>(nullable: false),
                    FunctionCallTransactionHash = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_function_call_parameters", x => new { x.parameter_order, x.transaction_hash });
                    table.ForeignKey(
                        name: "FK_function_call_parameters_function_calls_FunctionCallTransac~",
                        column: x => x.FunctionCallTransactionHash,
                        principalSchema: "quorum_explorer",
                        principalTable: "function_calls",
                        principalColumn: "transaction_hash",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "events",
                schema: "quorum_explorer",
                columns: table => new
                {
                    log_index = table.Column<long>(nullable: false),
                    transaction_hash = table.Column<string>(nullable: false),
                    event_name = table.Column<string>(nullable: false),
                    event_signature = table.Column<string>(nullable: false),
                    parameters_json = table.Column<string>(nullable: false),
                    block_timestamp = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => new { x.log_index, x.transaction_hash });
                    table.ForeignKey(
                        name: "FK_events_transaction_logs_log_index_transaction_hash",
                        columns: x => new { x.log_index, x.transaction_hash },
                        principalSchema: "quorum_explorer",
                        principalTable: "transaction_logs",
                        principalColumns: new[] { "log_index", "transaction_hash" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "event_parameters",
                schema: "quorum_explorer",
                columns: table => new
                {
                    log_index = table.Column<long>(nullable: false),
                    parameter_order = table.Column<int>(nullable: false),
                    transaction_hash = table.Column<string>(nullable: false),
                    parameter_name = table.Column<string>(nullable: false),
                    parameter_type = table.Column<string>(nullable: false),
                    parameter_value = table.Column<string>(nullable: false),
                    parameter_value_hash = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_event_parameters", x => new { x.log_index, x.parameter_order, x.transaction_hash });
                    table.ForeignKey(
                        name: "FK_event_parameters_events_log_index_transaction_hash",
                        columns: x => new { x.log_index, x.transaction_hash },
                        principalSchema: "quorum_explorer",
                        principalTable: "events",
                        principalColumns: new[] { "log_index", "transaction_hash" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ABIs_name",
                schema: "quorum_explorer",
                table: "ABIs",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_ABIs_type",
                schema: "quorum_explorer",
                table: "ABIs",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_event_parameters_transaction_hash",
                schema: "quorum_explorer",
                table: "event_parameters",
                column: "transaction_hash");

            migrationBuilder.CreateIndex(
                name: "IX_event_parameters_log_index_transaction_hash",
                schema: "quorum_explorer",
                table: "event_parameters",
                columns: new[] { "log_index", "transaction_hash" });

            migrationBuilder.CreateIndex(
                name: "IX_event_parameters_parameter_type_parameter_value_hash",
                schema: "quorum_explorer",
                table: "event_parameters",
                columns: new[] { "parameter_type", "parameter_value_hash" });

            migrationBuilder.CreateIndex(
                name: "IX_events_block_timestamp",
                schema: "quorum_explorer",
                table: "events",
                column: "block_timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_events_event_name",
                schema: "quorum_explorer",
                table: "events",
                column: "event_name");

            migrationBuilder.CreateIndex(
                name: "IX_events_event_signature",
                schema: "quorum_explorer",
                table: "events",
                column: "event_signature");

            migrationBuilder.CreateIndex(
                name: "IX_events_transaction_hash",
                schema: "quorum_explorer",
                table: "events",
                column: "transaction_hash");

            migrationBuilder.CreateIndex(
                name: "IX_events_log_index_transaction_hash",
                schema: "quorum_explorer",
                table: "events",
                columns: new[] { "log_index", "transaction_hash" });

            migrationBuilder.CreateIndex(
                name: "IX_events_block_timestamp_log_index_transaction_hash",
                schema: "quorum_explorer",
                table: "events",
                columns: new[] { "block_timestamp", "log_index", "transaction_hash" });

            migrationBuilder.CreateIndex(
                name: "IX_events_block_timestamp_log_index_transaction_hash_event_name",
                schema: "quorum_explorer",
                table: "events",
                columns: new[] { "block_timestamp", "log_index", "transaction_hash", "event_name" });

            migrationBuilder.CreateIndex(
                name: "IX_function_call_parameters_FunctionCallTransactionHash",
                schema: "quorum_explorer",
                table: "function_call_parameters",
                column: "FunctionCallTransactionHash");

            migrationBuilder.CreateIndex(
                name: "IX_function_call_parameters_parameter_type_parameter_value_hash",
                schema: "quorum_explorer",
                table: "function_call_parameters",
                columns: new[] { "parameter_type", "parameter_value_hash" });

            migrationBuilder.CreateIndex(
                name: "IX_function_calls_transaction_hash",
                schema: "quorum_explorer",
                table: "function_calls",
                column: "transaction_hash");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_logs_address",
                schema: "quorum_explorer",
                table: "transaction_logs",
                column: "address");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_logs_block_timestamp",
                schema: "quorum_explorer",
                table: "transaction_logs",
                column: "block_timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_logs_decoded",
                schema: "quorum_explorer",
                table: "transaction_logs",
                column: "decoded");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_logs_topic_0",
                schema: "quorum_explorer",
                table: "transaction_logs",
                column: "topic_0");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_logs_topic_1",
                schema: "quorum_explorer",
                table: "transaction_logs",
                column: "topic_1");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_logs_topic_2",
                schema: "quorum_explorer",
                table: "transaction_logs",
                column: "topic_2");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_logs_topic_3",
                schema: "quorum_explorer",
                table: "transaction_logs",
                column: "topic_3");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_logs_transaction_hash",
                schema: "quorum_explorer",
                table: "transaction_logs",
                column: "transaction_hash");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_block_hash",
                schema: "quorum_explorer",
                table: "transactions",
                column: "block_hash");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_block_number",
                schema: "quorum_explorer",
                table: "transactions",
                column: "block_number");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_block_timestamp",
                schema: "quorum_explorer",
                table: "transactions",
                column: "block_timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_contract_address",
                schema: "quorum_explorer",
                table: "transactions",
                column: "contract_address");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_from",
                schema: "quorum_explorer",
                table: "transactions",
                column: "from");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_status",
                schema: "quorum_explorer",
                table: "transactions",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_to",
                schema: "quorum_explorer",
                table: "transactions",
                column: "to");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_block_hash_transaction_index",
                schema: "quorum_explorer",
                table: "transactions",
                columns: new[] { "block_hash", "transaction_index" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transactions_from_nonce",
                schema: "quorum_explorer",
                table: "transactions",
                columns: new[] { "from", "nonce" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ABIs",
                schema: "quorum_explorer");

            migrationBuilder.DropTable(
                name: "blocks_data",
                schema: "quorum_explorer");

            migrationBuilder.DropTable(
                name: "event_parameters",
                schema: "quorum_explorer");

            migrationBuilder.DropTable(
                name: "function_call_parameters",
                schema: "quorum_explorer");

            migrationBuilder.DropTable(
                name: "events",
                schema: "quorum_explorer");

            migrationBuilder.DropTable(
                name: "function_calls",
                schema: "quorum_explorer");

            migrationBuilder.DropTable(
                name: "transaction_logs",
                schema: "quorum_explorer");

            migrationBuilder.DropTable(
                name: "transactions",
                schema: "quorum_explorer");
        }
    }
}
