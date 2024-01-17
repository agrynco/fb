using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddRestPasswordEmailTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OutgoingEmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "SubjectTemplate",
                value: "Підтвердження реєстрації");

            migrationBuilder.InsertData(
                table: "OutgoingEmailTemplates",
                columns: new[] { "Id", "BodyTemplate", "SubjectTemplate", "Type", "Updated" },
                values: new object[] { 2, "<header>\r\n    <h1>Скидання паролю</h1>\r\n    <meta charset=\"UTF-8\">\r\n    <style>\r\n        .greeting {\r\n            font-weight: bold;\r\n        }\r\n\r\n        .normal {\r\n            font-size: 1.2em;\r\n        }\r\n    </style>\r\n</header>\r\n\r\n<div class=\"normal\">\r\n    <p>Доброго дня, <span class=\"greeting\">{{Name}}!</span></i></p>\r\n\r\n    <p>Для відновлення паролю скористайтеся посиланням <a href=\"{{SiteUrl}}\">{{SiteName}}</a>.</p>\r\n\r\n    <p>Якщо ви не реєструвалися на сайті <a href=\"{{SiteUrl}}\">{{SiteName}}</a>, то просто проігноруйте цей лист.</p>\r\n</div>\r\n\r\n<p>З повагою, <a href=\"{{SiteUrl}}\">{{SiteName}}</a>.</p>", "Скидання паролю", 0, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OutgoingEmailTemplates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "OutgoingEmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "SubjectTemplate",
                value: "Activating new user");
        }
    }
}
