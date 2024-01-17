using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Users",
                type: "datetime(6)",
                nullable: true,
                defaultValueSql: "(UTC_TIMESTAMP)",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Transactions",
                type: "datetime(6)",
                nullable: true,
                defaultValueSql: "(UTC_TIMESTAMP)",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "TransactionCategories",
                type: "datetime(6)",
                nullable: true,
                defaultValueSql: "(UTC_TIMESTAMP)",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "RefreshTokens",
                type: "datetime(6)",
                nullable: true,
                defaultValueSql: "(UTC_TIMESTAMP)",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "OutgoingEmailTemplates",
                type: "datetime(6)",
                nullable: true,
                defaultValueSql: "(UTC_TIMESTAMP)",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "OutgoingEmails",
                type: "datetime(6)",
                nullable: true,
                defaultValueSql: "(UTC_TIMESTAMP)",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "FamilyMembers",
                type: "datetime(6)",
                nullable: true,
                defaultValueSql: "(UTC_TIMESTAMP)",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Currencies",
                type: "datetime(6)",
                nullable: true,
                defaultValueSql: "(UTC_TIMESTAMP)",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Countries",
                type: "datetime(6)",
                nullable: true,
                defaultValueSql: "(UTC_TIMESTAMP)",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Banks",
                type: "datetime(6)",
                nullable: true,
                defaultValueSql: "(UTC_TIMESTAMP)",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Accounts",
                type: "datetime(6)",
                nullable: true,
                defaultValueSql: "(UTC_TIMESTAMP)",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP)"),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true, defaultValueSql: "(UTC_TIMESTAMP)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TranslatedStrings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TranslatedStrings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TranslatedStrings_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Key" },
                values: new object[,]
                {
                    { 1, "ua" },
                    { 2, "en" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Key",
                table: "Languages",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TranslatedStrings_LanguageId",
                table: "TranslatedStrings",
                column: "LanguageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TranslatedStrings");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Users",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldDefaultValueSql: "(UTC_TIMESTAMP)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Transactions",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldDefaultValueSql: "(UTC_TIMESTAMP)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "TransactionCategories",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldDefaultValueSql: "(UTC_TIMESTAMP)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "RefreshTokens",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldDefaultValueSql: "(UTC_TIMESTAMP)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "OutgoingEmailTemplates",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldDefaultValueSql: "(UTC_TIMESTAMP)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "OutgoingEmails",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldDefaultValueSql: "(UTC_TIMESTAMP)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "FamilyMembers",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldDefaultValueSql: "(UTC_TIMESTAMP)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Currencies",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldDefaultValueSql: "(UTC_TIMESTAMP)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Countries",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldDefaultValueSql: "(UTC_TIMESTAMP)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Banks",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldDefaultValueSql: "(UTC_TIMESTAMP)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Accounts",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true,
                oldDefaultValueSql: "(UTC_TIMESTAMP)");
        }
    }
}
