using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddTypeToAccountUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_Name_OwnerId_CurrencyId",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Name_OwnerId_CurrencyId_Type",
                table: "Accounts",
                columns: new[] { "Name", "OwnerId", "CurrencyId", "Type" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_Name_OwnerId_CurrencyId_Type",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Name_OwnerId_CurrencyId",
                table: "Accounts",
                columns: new[] { "Name", "OwnerId", "CurrencyId" },
                unique: true);
        }
    }
}
