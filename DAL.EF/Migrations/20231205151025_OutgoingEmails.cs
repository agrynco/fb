using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class OutgoingEmails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_EmailTemplates_TemplateId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Users_UserId",
                table: "Emails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailTemplates",
                table: "EmailTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Emails",
                table: "Emails");

            migrationBuilder.RenameTable(
                name: "EmailTemplates",
                newName: "OutgoingEmailTemplates");

            migrationBuilder.RenameTable(
                name: "Emails",
                newName: "OutgoingEmails");

            migrationBuilder.RenameIndex(
                name: "IX_EmailTemplates_SubjectTemplate",
                table: "OutgoingEmailTemplates",
                newName: "IX_OutgoingEmailTemplates_SubjectTemplate");

            migrationBuilder.RenameIndex(
                name: "IX_Emails_UserId",
                table: "OutgoingEmails",
                newName: "IX_OutgoingEmails_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Emails_TemplateId",
                table: "OutgoingEmails",
                newName: "IX_OutgoingEmails_TemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_Emails_Subject_UserId",
                table: "OutgoingEmails",
                newName: "IX_OutgoingEmails_Subject_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutgoingEmailTemplates",
                table: "OutgoingEmailTemplates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OutgoingEmails",
                table: "OutgoingEmails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OutgoingEmails_OutgoingEmailTemplates_TemplateId",
                table: "OutgoingEmails",
                column: "TemplateId",
                principalTable: "OutgoingEmailTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OutgoingEmails_Users_UserId",
                table: "OutgoingEmails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OutgoingEmails_OutgoingEmailTemplates_TemplateId",
                table: "OutgoingEmails");

            migrationBuilder.DropForeignKey(
                name: "FK_OutgoingEmails_Users_UserId",
                table: "OutgoingEmails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OutgoingEmailTemplates",
                table: "OutgoingEmailTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OutgoingEmails",
                table: "OutgoingEmails");

            migrationBuilder.RenameTable(
                name: "OutgoingEmailTemplates",
                newName: "EmailTemplates");

            migrationBuilder.RenameTable(
                name: "OutgoingEmails",
                newName: "Emails");

            migrationBuilder.RenameIndex(
                name: "IX_OutgoingEmailTemplates_SubjectTemplate",
                table: "EmailTemplates",
                newName: "IX_EmailTemplates_SubjectTemplate");

            migrationBuilder.RenameIndex(
                name: "IX_OutgoingEmails_UserId",
                table: "Emails",
                newName: "IX_Emails_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OutgoingEmails_TemplateId",
                table: "Emails",
                newName: "IX_Emails_TemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_OutgoingEmails_Subject_UserId",
                table: "Emails",
                newName: "IX_Emails_Subject_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailTemplates",
                table: "EmailTemplates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Emails",
                table: "Emails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_EmailTemplates_TemplateId",
                table: "Emails",
                column: "TemplateId",
                principalTable: "EmailTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Users_UserId",
                table: "Emails",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
