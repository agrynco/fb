using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddedAFewMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Balance",
                value: 85160m);

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "ActorId", "Amount", "CategoryId", "Description", "ExchangeRate", "TransactionCorrelationId", "TransactionDateTime", "Updated" },
                values: new object[,]
                {
                    { 1, 1, 1, 100000m, null, "Income", 1m, null, new DateTime(2023, 5, 1, 0, 1, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 1, 1, -2400m, 2, "Продукти", 1m, null, new DateTime(2023, 5, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, 1, 1, -200m, 19, "Інтернет", 1m, null, new DateTime(2023, 5, 7, 11, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 4, 1, 1, -440m, 4, "Шашлик", 1m, null, new DateTime(2023, 5, 15, 14, 30, 0, 0, DateTimeKind.Unspecified), null },
                    { 5, 1, 1, -1450m, 50, "Газ", 1m, null, new DateTime(2023, 5, 15, 14, 30, 0, 0, DateTimeKind.Unspecified), null },
                    { 6, 1, 1, -350m, 17, "Холодна вода", 1m, null, new DateTime(2023, 5, 28, 14, 30, 0, 0, DateTimeKind.Unspecified), null },
                    { 7, 1, 1, -1000m, 22, "Консерви", 1m, null, new DateTime(2023, 5, 29, 10, 15, 0, 0, DateTimeKind.Unspecified), null }
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Balance",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "EmailConfirmationToken",
                value: "dac76ef8-1f22-45ca-af38-df0d030fcd22");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "EmailConfirmationToken",
                value: "4a1c701b-d65f-45de-ae65-e71a610df1b9");
        }
    }
}
