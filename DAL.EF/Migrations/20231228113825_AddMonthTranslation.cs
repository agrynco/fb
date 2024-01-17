using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddMonthTranslation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TranslatedStrings");

            migrationBuilder.CreateTable(
                name: "MothTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    MonthNumber = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP)"),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true, defaultValueSql: "(UTC_TIMESTAMP)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MothTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MothTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "MothTranslations",
                columns: new[] { "Id", "LanguageId", "MonthNumber", "Value" },
                values: new object[,]
                {
                    { 1, 1, 1, "Січень" },
                    { 2, 1, 2, "Лютий" },
                    { 3, 1, 3, "Березень" },
                    { 4, 1, 4, "Квітень" },
                    { 5, 1, 5, "Травень" },
                    { 6, 1, 6, "Червень" },
                    { 7, 1, 7, "Липень" },
                    { 8, 1, 8, "Серпень" },
                    { 9, 1, 9, "Вересень" },
                    { 10, 1, 10, "Жовтень" },
                    { 11, 1, 11, "Листопад" },
                    { 12, 1, 12, "Грудень" },
                    { 13, 2, 1, "January" },
                    { 14, 2, 2, "February" },
                    { 15, 2, 3, "March" },
                    { 16, 2, 4, "April" },
                    { 17, 2, 5, "May" },
                    { 18, 2, 6, "June" },
                    { 19, 2, 7, "July" },
                    { 20, 2, 8, "August" },
                    { 21, 2, 9, "September" },
                    { 22, 2, 10, "October" },
                    { 23, 2, 11, "November" },
                    { 24, 2, 12, "December" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MothTranslations_LanguageId_MonthNumber",
                table: "MothTranslations",
                columns: new[] { "LanguageId", "MonthNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MothTranslations_LanguageId_Value_MonthNumber",
                table: "MothTranslations",
                columns: new[] { "LanguageId", "Value", "MonthNumber" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MothTranslations");

            migrationBuilder.CreateTable(
                name: "TranslatedStrings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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

            migrationBuilder.CreateIndex(
                name: "IX_TranslatedStrings_LanguageId",
                table: "TranslatedStrings",
                column: "LanguageId");
        }
    }
}
