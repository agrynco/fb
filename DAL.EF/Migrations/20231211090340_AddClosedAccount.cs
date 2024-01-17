using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EF.Migrations
{
    using System.Diagnostics;

    /// <inheritdoc />
    public partial class AddClosedAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.OnlyForIntegrationTests(() => 
            {
                migrationBuilder.InsertData(
                    table: "Accounts",
                    columns: new[] { "Id", "Balance", "Closed", "CurrencyId", "Description", "Name", "OwnerId", "Type", "Updated" },
                    values: new object[] { 13, 0m, true, 3, null, "Closed account", 3, 3, null });

                migrationBuilder.InsertData(
                    table: "CashAccounts",
                    column: "Id",
                    value: 13);
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CashAccounts",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 13);
        }
    }
}
