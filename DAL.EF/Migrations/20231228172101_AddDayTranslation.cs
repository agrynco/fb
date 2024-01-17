using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddDayTranslation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    DayNumber = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP)"),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true, defaultValueSql: "(UTC_TIMESTAMP)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "DayTranslations",
                columns: new[] { "Id", "DayNumber", "LanguageId", "Value" },
                values: new object[,]
                {
                    { 1, 1, 1, "Понеділок" },
                    { 2, 2, 1, "Вівторок" },
                    { 3, 3, 1, "Середа" },
                    { 4, 4, 1, "Четвер" },
                    { 5, 5, 1, "П'ятниця" },
                    { 6, 6, 1, "Субота" },
                    { 7, 7, 1, "Неділя" },
                    { 8, 1, 2, "Monday" },
                    { 9, 2, 2, "Tuesday" },
                    { 10, 3, 2, "Wednesday" },
                    { 11, 4, 2, "Thursday" },
                    { 12, 5, 2, "Friday" },
                    { 13, 6, 2, "Saturday" },
                    { 14, 7, 2, "Sunday" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayTranslations_LanguageId_DayNumber",
                table: "DayTranslations",
                columns: new[] { "LanguageId", "DayNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DayTranslations_LanguageId_Value_DayNumber",
                table: "DayTranslations",
                columns: new[] { "LanguageId", "Value", "DayNumber" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayTranslations");
        }
    }
}
