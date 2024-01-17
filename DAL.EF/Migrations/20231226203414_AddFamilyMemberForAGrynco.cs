using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddFamilyMemberForAGrynco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.OnlyForIntegrationTests(() => migrationBuilder.InsertData(
                table: "FamilyMembers",
                columns: new[] { "Id", "Name", "OwnerId", "Updated" },
                values: new object[,]
                {
                    { 18, "Test 1", 1, null },
                    { 19, "Test 2", 1, null }
                }));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "FamilyMembers",
                keyColumn: "Id",
                keyValue: 19);
        }
    }
}
