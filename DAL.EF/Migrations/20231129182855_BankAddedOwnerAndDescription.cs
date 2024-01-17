using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class BankAddedOwnerAndDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Banks_Name",
                table: "Banks");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Banks",
                type: "longtext",
                nullable: true,
                defaultValue: null);
                
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Banks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "OwnerId" },
                values: new object[] { null, 1 });

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "OwnerId" },
                values: new object[] { null, 1 });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "Description", "Name", "OwnerId", "Updated" },
                values: new object[,]
                {
                    { 3, null, "Приват банк", 3, null },
                    { 4, null, "MONO банк", 3, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banks_OwnerId_Name",
                table: "Banks",
                columns: new[] { "OwnerId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Banks_OwnerId_Name",
                table: "Banks");

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Banks");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_Name",
                table: "Banks",
                column: "Name",
                unique: true);
        }
    }
}
