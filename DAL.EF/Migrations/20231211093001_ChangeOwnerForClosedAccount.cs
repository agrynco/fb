using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOwnerForClosedAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.OnlyForIntegrationTests(() =>
            {
                migrationBuilder.UpdateData(
                    table: "Accounts",
                    keyColumn: "Id",
                    keyValue: 13,
                    column: "OwnerId",
                    value: 1);
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 13,
                column: "OwnerId",
                value: 3);
        }
    }
}
