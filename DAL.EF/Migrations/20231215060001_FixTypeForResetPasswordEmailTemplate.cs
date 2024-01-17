using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class FixTypeForResetPasswordEmailTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OutgoingEmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "BodyTemplate",
                value: "<header>\r\n    <h1>Підтвердження реєстрації</h1>\r\n    <meta charset=\"UTF-8\">\r\n    <style>\r\n        .greeting {\r\n            font-weight: bold;\r\n        }\r\n\r\n        .normal {\r\n            font-size: 1.2em;\r\n        }\r\n    </style>\r\n</header>\r\n\r\n<div class=\"normal\">\r\n    <p>Доброго дня, <span class=\"greeting\">{{FirstName}} {{LastName}}!</span></i></p>\r\n\r\n    <p>Ви зареєструвалися на сайті <a href=\"{{SiteUrl}}\">{{SiteName}}</a>.</p>\r\n\r\n    <p>Для підтвердження реєстрації перейдіть за посиланням: <a href=\"{{SiteUrl}}/confirm-email?token={{EmailConfirmationToken}}\">{SiteUrl}}/confirm-email?token={{EmailConfirmationToken}}</a></p>\r\n\r\n    <p>Якщо ви не реєструвалися на сайті <a href=\"{{SiteUrl}}\">{{SiteName}}</a>, то просто проігноруйте цей лист.</p>\r\n</div>\r\n\r\n<p>З повагою, <a href=\"{{SiteUrl}}\">{{SiteName}}</a>.</p>");

            migrationBuilder.UpdateData(
                table: "OutgoingEmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BodyTemplate", "Type" },
                values: new object[] { "<header>\r\n    <h1>Скидання паролю</h1>\r\n    <meta charset=\"UTF-8\">\r\n    <style>\r\n        .greeting {\r\n            font-weight: bold;\r\n        }\r\n\r\n        .normal {\r\n            font-size: 1.2em;\r\n        }\r\n    </style>\r\n</header>\r\n\r\n<div class=\"normal\">\r\n    <p>Доброго дня, <span class=\"greeting\">{{FirstName}} {{LastName}}!</span></i></p>\r\n\r\n    <p>Для відновлення паролю скористайтеся посиланням <a href=\"{{SiteUrl}}/forgot-password?token={{EmailConfirmationToken}}\">{{SiteName}}</a>.</p>\r\n\r\n    <p>Якщо ви не реєструвалися на сайті <a href=\"{{SiteUrl}}\">{{SiteName}}</a>, то просто проігноруйте цей лист.</p>\r\n</div>\r\n\r\n<p>З повагою, <a href=\"{{SiteUrl}}\">{{SiteName}}</a>.</p>", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "OutgoingEmailTemplates",
                keyColumn: "Id",
                keyValue: 1,
                column: "BodyTemplate",
                value: "<header>\r\n    <h1>Підтвердження реєстрації</h1>\r\n    <meta charset=\"UTF-8\">\r\n    <style>\r\n        .greeting {\r\n            font-weight: bold;\r\n        }\r\n\r\n        .normal {\r\n            font-size: 1.2em;\r\n        }\r\n    </style>\r\n</header>\r\n\r\n<div class=\"normal\">\r\n    <p>Доброго дня, <span class=\"greeting\">{{Name}}!</span></i></p>\r\n\r\n    <p>Ви зареєструвалися на сайті <a href=\"{{SiteUrl}}\">{{SiteName}}</a>.</p>\r\n\r\n    <p>Для підтвердження реєстрації перейдіть за посиланням: <a href=\"{{ConfirmUrl}}\">{{ConfirmUrl}}</a></p>\r\n\r\n    <p>Якщо ви не реєструвалися на сайті <a href=\"{{SiteUrl}}\">{{SiteName}}</a>, то просто проігноруйте цей лист.</p>\r\n</div>\r\n\r\n<p>З повагою, <a href=\"{{SiteUrl}}\">{{SiteName}}</a>.</p>");

            migrationBuilder.UpdateData(
                table: "OutgoingEmailTemplates",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BodyTemplate", "Type" },
                values: new object[] { "<header>\r\n    <h1>Скидання паролю</h1>\r\n    <meta charset=\"UTF-8\">\r\n    <style>\r\n        .greeting {\r\n            font-weight: bold;\r\n        }\r\n\r\n        .normal {\r\n            font-size: 1.2em;\r\n        }\r\n    </style>\r\n</header>\r\n\r\n<div class=\"normal\">\r\n    <p>Доброго дня, <span class=\"greeting\">{{Name}}!</span></i></p>\r\n\r\n    <p>Для відновлення паролю скористайтеся посиланням <a href=\"{{SiteUrl}}\">{{SiteName}}</a>.</p>\r\n\r\n    <p>Якщо ви не реєструвалися на сайті <a href=\"{{SiteUrl}}\">{{SiteName}}</a>, то просто проігноруйте цей лист.</p>\r\n</div>\r\n\r\n<p>З повагою, <a href=\"{{SiteUrl}}\">{{SiteName}}</a>.</p>", 0 });
        }
    }
}
