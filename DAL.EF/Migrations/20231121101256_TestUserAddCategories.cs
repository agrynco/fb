using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class TestUserAddCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TransactionCategories",
                columns: new[] { "Id", "Description", "Name", "OwnerId", "ParentId", "Updated" },
                values: new object[,]
                {
                    { 53, null, "Прибутки", 3, null, null },
                    { 54, null, "Продукти", 3, null, null },
                    { 61, null, "Побутова техніка", 3, null, null },
                    { 62, null, "Особисте здоров'я", 3, null, null },
                    { 63, null, "Відпочинок", 3, null, null },
                    { 64, null, "Навчання", 3, null, null },
                    { 65, null, "Одежа", 3, null, null },
                    { 66, null, "Комунальні послуги", 3, null, null },
                    { 73, null, "Домашні тварини", 3, null, null },
                    { 76, null, "Транспорт", 3, null, null },
                    { 55, null, "Молочні", 3, 54, null },
                    { 56, null, "М'ясні", 3, 54, null },
                    { 57, null, "Хлібобулочні", 3, 54, null },
                    { 58, null, "Овочі", 3, 54, null },
                    { 59, null, "Фрукти", 3, 54, null },
                    { 60, null, "Ягоди", 3, 54, null },
                    { 67, null, "Опалення", 3, 66, null },
                    { 68, null, "Гаряча вода", 3, 66, null },
                    { 69, null, "Холодна вода", 3, 66, null },
                    { 70, null, "Електроенергія", 3, 66, null },
                    { 71, null, "Інтернет", 3, 66, null },
                    { 72, null, "Мобільний зв'язок", 3, 66, null },
                    { 74, null, "Кіт", 3, 73, null },
                    { 75, null, "Хом'як", 3, 73, null },
                    { 77, null, "Автомобіль", 3, 76, null },
                    { 78, null, "Витратні матеріали", 3, 77, null },
                    { 93, null, "Обслуговування", 3, 77, null },
                    { 101, null, "Паливо", 3, 77, null },
                    { 79, null, "Тормозні колодки", 3, 78, null },
                    { 82, null, "Покришки", 3, 78, null },
                    { 85, null, "Рідини", 3, 78, null },
                    { 89, null, "Фільтри", 3, 78, null },
                    { 94, null, "Заміна покришок (зима/літо)", 3, 93, null },
                    { 95, null, "Техогляд (кожні 10 тис. км.)", 3, 93, null },
                    { 102, null, "Газ", 3, 101, null },
                    { 103, null, "Бензин", 3, 101, null },
                    { 80, null, "Передні", 3, 79, null },
                    { 81, null, "Задні", 3, 79, null },
                    { 83, null, "Зимові", 3, 82, null },
                    { 84, null, "Літні", 3, 82, null },
                    { 86, null, "Омивайка", 3, 85, null },
                    { 87, null, "Олія моторна", 3, 85, null },
                    { 88, null, "Олія коробки передач", 3, 85, null },
                    { 90, null, "Салон", 3, 89, null },
                    { 91, null, "Фільтр олії (двигун)", 3, 89, null },
                    { 92, null, "Повітряний фільтр", 3, 89, null },
                    { 96, null, "Заміна тормозних колодок", 3, 95, null },
                    { 97, null, "Заміна масла двигуна", 3, 95, null },
                    { 98, null, "Заміна фільтра олії (двигун)", 3, 95, null },
                    { 99, null, "Заміна зовнішнього повітряного фільтра", 3, 95, null },
                    { 100, null, "ГБО", 3, 95, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "TransactionCategories",
                keyColumn: "Id",
                keyValue: 76);
        }
    }
}
