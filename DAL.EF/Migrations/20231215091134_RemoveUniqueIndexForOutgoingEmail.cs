using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueIndexForOutgoingEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OutgoingEmails_Subject_UserId",
                table: "OutgoingEmails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OutgoingEmails_Subject_UserId",
                table: "OutgoingEmails",
                columns: new[] { "Subject", "UserId" },
                unique: true);
        }
    }
}
