using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class TestUserAddAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Balance", "Closed", "CurrencyId", "Description", "Name", "OwnerId", "Purpose", "Type", "Updated" },
                values: new object[,]
                {
                    { 10, 85160m, false, 1, null, "Накопичення", 3, null, 3, null },
                    { 11, 0m, false, 2, null, "Накопичення", 3, null, 3, null },
                    { 12, 0m, false, 3, null, "Накопичення", 3, null, 3, null }
                });

            migrationBuilder.InsertData(
                table: "CashAccounts",
                column: "Id",
                values: new object[]
                {
                    10,
                    11,
                    12
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CashAccounts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CashAccounts",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CashAccounts",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
