using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionCorrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Transactions_CorrelatedTransactionId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "CorrelatedTransactionId",
                table: "Transactions",
                newName: "TransactionCorrelationId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CorrelatedTransactionId",
                table: "Transactions",
                newName: "IX_Transactions_TransactionCorrelationId");

            migrationBuilder.CreateTable(
                name: "TransactionCorrelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TransactionAId = table.Column<int>(type: "int", nullable: false),
                    TransactionBId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCorrelations", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCorrelations_TransactionAId",
                table: "TransactionCorrelations",
                column: "TransactionAId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCorrelations_TransactionBId",
                table: "TransactionCorrelations",
                column: "TransactionBId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionCorrelations_TransactionCorrelationId",
                table: "Transactions",
                column: "TransactionCorrelationId",
                principalTable: "TransactionCorrelations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionCorrelations_TransactionCorrelationId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionCorrelations");

            migrationBuilder.RenameColumn(
                name: "TransactionCorrelationId",
                table: "Transactions",
                newName: "CorrelatedTransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_TransactionCorrelationId",
                table: "Transactions",
                newName: "IX_Transactions_CorrelatedTransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Transactions_CorrelatedTransactionId",
                table: "Transactions",
                column: "CorrelatedTransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }
    }
}
