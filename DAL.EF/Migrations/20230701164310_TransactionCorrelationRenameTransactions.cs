using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class TransactionCorrelationRenameTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionBId",
                table: "TransactionCorrelations",
                newName: "OutcomeTransactionId");

            migrationBuilder.RenameColumn(
                name: "TransactionAId",
                table: "TransactionCorrelations",
                newName: "IncomeTransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionCorrelations_TransactionBId",
                table: "TransactionCorrelations",
                newName: "IX_TransactionCorrelations_OutcomeTransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionCorrelations_TransactionAId",
                table: "TransactionCorrelations",
                newName: "IX_TransactionCorrelations_IncomeTransactionId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "EmailConfirmationToken",
                value: "b17527e9-ff7e-4544-8f6b-2b32a9d3b5ae");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "EmailConfirmationToken",
                value: "233d0be5-7ba0-48c9-b3be-da0ecd2c6035");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OutcomeTransactionId",
                table: "TransactionCorrelations",
                newName: "TransactionBId");

            migrationBuilder.RenameColumn(
                name: "IncomeTransactionId",
                table: "TransactionCorrelations",
                newName: "TransactionAId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionCorrelations_OutcomeTransactionId",
                table: "TransactionCorrelations",
                newName: "IX_TransactionCorrelations_TransactionBId");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionCorrelations_IncomeTransactionId",
                table: "TransactionCorrelations",
                newName: "IX_TransactionCorrelations_TransactionAId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "EmailConfirmationToken",
                value: "afb566ad-4e1c-4f98-8ea0-c240eeb4da21");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "EmailConfirmationToken",
                value: "f1aea7df-364d-4ff9-a318-88e04d5a77d6");
        }
    }
}
