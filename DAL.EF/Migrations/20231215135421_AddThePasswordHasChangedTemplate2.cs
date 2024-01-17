using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddThePasswordHasChangedTemplate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "OutgoingEmailTemplates",
                columns: new[] { "Id", "BodyTemplate", "SubjectTemplate", "Type", "Updated" },
                values: new object[] { 3, "<header>\r\n    <h1>Пароль змінено</h1>\r\n    <meta charset=\"UTF-8\">\r\n    <style>\r\n        .greeting {\r\n            font-weight: bold;\r\n        }\r\n\r\n        .normal {\r\n            font-size: 1.2em;\r\n        }\r\n    </style>\r\n</header>\r\n\r\n<div class=\"normal\">\r\n    <p>Доброго дня, <span class=\"greeting\">{{FirstName}} {{LastName}}!</span></i></p>\r\n\r\n    <p>Пароль змінено. Для входу на сайт скористайтеся посиланням <a\r\n            href=\"{{SiteUrl}}/sign-in\">{{SiteUrl}}/sign-in</a>.\r\n    </p>\r\n\r\n    <p>Якщо ви не реєструвалися на сайті <a href=\"{{SiteUrl}}\">{{SiteName}}</a>, то просто проігноруйте цей лист.</p>\r\n</div>\r\n\r\n<p>З повагою, <a href=\"{{SiteUrl}}\">{{SiteName}}</a>.</p>", "Пароль змінено", 2, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OutgoingEmailTemplates",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
