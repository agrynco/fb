using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddTestUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Activated", "Email", "EmailConfirmationToken", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Updated", "Username" },
                values: new object[] { 3, true, "fb-test@i.ua", "642584EE-0DCF-4D3A-A485-0AFCF81B00C8", "Fbtest", "Fbtest", "e2ammRNIGDeuxRu2h+pIHXAuT4tRmcaGYwlofzmEe8I=", new byte[] { 203, 35, 198, 123, 209, 53, 124, 142, 67, 181, 18, 72, 108, 105, 132, 33 }, null, "test" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
