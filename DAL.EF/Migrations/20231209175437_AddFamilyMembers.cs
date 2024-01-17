using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddFamilyMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FamilyMemberId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FamilyMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP)"),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyMembers_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "FamilyMembers",
                columns: new[] { "Id", "Name", "OwnerId", "Updated" },
                values: new object[,]
                {
                    { 1, "Мама", 3, null },
                    { 2, "Тато", 3, null },
                    { 3, "Я", 3, null },
                    { 4, "Брат", 3, null },
                    { 5, "Сестра", 3, null },
                    { 6, "Дідусь", 3, null },
                    { 7, "Бабуся", 3, null },
                    { 8, "Дядько", 3, null },
                    { 9, "Тітка", 3, null },
                    { 10, "Кузен", 3, null },
                    { 11, "Кузина", 3, null },
                    { 12, "Племінник", 3, null },
                    { 13, "Племінниця", 3, null },
                    { 14, "Двоюрідний брат", 3, null },
                    { 15, "Двоюрідна сестра", 3, null },
                    { 16, "Двоюрідний дідусь", 3, null },
                    { 17, "Двоюрідна бабуся", 3, null }
                });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "FamilyMemberId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "FamilyMemberId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 3,
                column: "FamilyMemberId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 4,
                column: "FamilyMemberId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 5,
                column: "FamilyMemberId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 6,
                column: "FamilyMemberId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 7,
                column: "FamilyMemberId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FamilyMemberId",
                table: "Transactions",
                column: "FamilyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_Name_OwnerId",
                table: "FamilyMembers",
                columns: new[] { "Name", "OwnerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_OwnerId",
                table: "FamilyMembers",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_FamilyMembers_FamilyMemberId",
                table: "Transactions",
                column: "FamilyMemberId",
                principalTable: "FamilyMembers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_FamilyMembers_FamilyMemberId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "FamilyMembers");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_FamilyMemberId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FamilyMemberId",
                table: "Transactions");
        }
    }
}
