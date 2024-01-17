using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP)"),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP)"),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsoCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SymbolOrAbbrev = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP)"),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Activated = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Email = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "varchar(75)", maxLength: 75, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(75)", maxLength: 75, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordHash = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(250)", maxLength: 250, nullable: false),
                    Username = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP)"),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CountryCurrency",
                columns: table => new
                {
                    CountriesId = table.Column<int>(type: "int", nullable: false),
                    CurrenciesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryCurrency", x => new { x.CountriesId, x.CurrenciesId });
                    table.ForeignKey(
                        name: "FK_CountryCurrency_Countries_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryCurrency_Currencies_CurrenciesId",
                        column: x => x.CurrenciesId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActualBalance = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Closed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    Purpose = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP)"),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP)"),
                    CreatedByIp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Expires = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ReasonRevoked = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ReplacedByToken = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Revoked = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    RevokedByIp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Token = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TransactionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP)"),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionCategories_TransactionCategories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TransactionCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransactionCategories_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    IBAN = table.Column<string>(type: "varchar(34)", maxLength: 34, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Accounts_Id",
                        column: x => x.Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CardAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    CardNumber = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardAccounts_Accounts_Id",
                        column: x => x.Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardAccounts_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CashAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashAccounts_Accounts_Id",
                        column: x => x.Id,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    ActorId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CorrelatedTransactionId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExchangeRate = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TransactionDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "(UTC_TIMESTAMP)"),
                    Updated = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TransactionCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Transactions_CorrelatedTransactionId",
                        column: x => x.CorrelatedTransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Users_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "Name", "Updated" },
                values: new object[,]
                {
                    { 1, "Приват банк", null },
                    { 2, "MONO банк", null }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "Updated" },
                values: new object[,]
                {
                    { 1, "Австрія", null },
                    { 2, "Бельгія", null },
                    { 3, "Болгарія", null },
                    { 4, "Венгрія", null },
                    { 5, "Німеччина", null },
                    { 6, "Греція", null },
                    { 7, "Данія", null },
                    { 8, "Ірландія", null },
                    { 9, "Іспанія", null },
                    { 10, "Італія", null },
                    { 11, "Кіпр", null },
                    { 12, "Латвія", null },
                    { 13, "Литва", null },
                    { 14, "Люксембург", null },
                    { 15, "Мальта", null },
                    { 16, "Нідерланди", null },
                    { 17, "Польща", null },
                    { 18, "Португалія", null },
                    { 19, "Руминія", null },
                    { 20, "Словакія", null },
                    { 21, "Словенія", null },
                    { 22, "Фінляндія", null },
                    { 23, "Франція", null },
                    { 24, "Хорватія", null },
                    { 25, "Чехія", null },
                    { 26, "Швеція", null },
                    { 27, "Естонія", null },
                    { 28, "Україна", null },
                    { 29, "США", null }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "IsoCode", "Name", "SymbolOrAbbrev", "Updated" },
                values: new object[,]
                {
                    { 1, "UAH", "Українська гривня", "₴", null },
                    { 2, "EUR", "Євро", "€", null },
                    { 3, "USD", "Долар США", "$", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Activated", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Updated", "Username" },
                values: new object[,]
                {
                    { 1, true, "agrynco@gmail.com", "Anatolii", "Hrynchuk", "6QqKKDVyflm2tQyTo2OxDp2WOjmPldJxyIV7HNsJeE0=", new byte[] { 195, 211, 51, 65, 6, 97, 224, 29, 88, 12, 22, 76, 116, 140, 249, 214 }, null, "agrynco" },
                    { 2, true, "family.budget.2023@gmail.com", "Family", "Budget", "6QqKKDVyflm2tQyTo2OxDp2WOjmPldJxyIV7HNsJeE0=", new byte[] { 195, 211, 51, 65, 6, 97, 224, 29, 88, 12, 22, 76, 116, 140, 249, 214 }, null, "family.budget" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "ActualBalance", "Closed", "CurrencyId", "Description", "Name", "OwnerId", "Purpose", "Updated" },
                values: new object[,]
                {
                    { 1, 0m, false, 1, null, "Накопичення", 1, null, null },
                    { 2, 0m, false, 2, null, "Накопичення", 1, null, null },
                    { 3, 0m, false, 3, null, "Накопичення", 1, null, null },
                    { 4, 0m, false, 1, null, "Карта ключ до рахунку", 1, null, null },
                    { 5, 0m, false, 1, null, "Звичайна картка", 1, null, null },
                    { 6, 0m, false, 1, null, "Чорна картка", 1, null, null },
                    { 7, 0m, false, 1, null, "Біла картка", 1, null, null },
                    { 9, 0m, false, 2, null, "ФОП", 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "TransactionCategories",
                columns: new[] { "Id", "Description", "Name", "OwnerId", "ParentId", "Updated" },
                values: new object[,]
                {
                    { 1, null, "Прибутки", 1, null, null },
                    { 2, null, "Продукти", 1, null, null },
                    { 9, null, "Побутова техніка", 1, null, null },
                    { 10, null, "Особисте здоров'я", 1, null, null },
                    { 11, null, "Відпочинок", 1, null, null },
                    { 12, null, "Навчання", 1, null, null },
                    { 13, null, "Одежа", 1, null, null },
                    { 14, null, "Комунальні послуги", 1, 14, null },
                    { 21, null, "Домашні тварини", 1, null, null },
                    { 24, null, "Транспорт", 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "BankId", "IBAN" },
                values: new object[] { 9, 2, "UA223220010000026005330066082" });

            migrationBuilder.InsertData(
                table: "CardAccounts",
                columns: new[] { "Id", "BankId", "CardNumber" },
                values: new object[,]
                {
                    { 4, 1, "5169330523068218" },
                    { 5, 1, "5375411412366993" },
                    { 6, 2, "5375414144891016" },
                    { 7, 2, "537541508625303" }
                });

            migrationBuilder.InsertData(
                table: "CashAccounts",
                column: "Id",
                values: new object[]
                {
                    1,
                    2,
                    3
                });

            migrationBuilder.InsertData(
                table: "TransactionCategories",
                columns: new[] { "Id", "Description", "Name", "OwnerId", "ParentId", "Updated" },
                values: new object[,]
                {
                    { 3, null, "Молочні", 1, 2, null },
                    { 4, null, "М'ясні", 1, 2, null },
                    { 5, null, "Хлібобулочні", 1, 2, null },
                    { 6, null, "Овочі", 1, 2, null },
                    { 7, null, "Фрукти", 1, 2, null },
                    { 8, null, "Ягоди", 1, 2, null },
                    { 15, null, "Опалення", 1, 14, null },
                    { 16, null, "Гаряча вода", 1, 14, null },
                    { 17, null, "Холодна вода", 1, 14, null },
                    { 18, null, "Електроенергія", 1, 14, null },
                    { 19, null, "Інтернет", 1, 14, null },
                    { 20, null, "Мобільний зв'язок", 1, 14, null },
                    { 22, null, "Кіт", 1, 21, null },
                    { 23, null, "Хом'як", 1, 21, null },
                    { 25, null, "Автомобіль", 1, 24, null },
                    { 26, null, "Витратні матеріали", 1, 25, null },
                    { 41, null, "Обслуговування", 1, 25, null },
                    { 49, null, "Паливо", 1, 25, null },
                    { 27, null, "Тормозні колодки", 1, 26, null },
                    { 30, null, "Покришки", 1, 26, null },
                    { 33, null, "Рідини", 1, 26, null },
                    { 37, null, "Фільтри", 1, 26, null },
                    { 42, null, "Заміна покришок (зима/літо)", 1, 41, null },
                    { 43, null, "Техогляд (кожні 10 тис. км.)", 1, 41, null },
                    { 50, null, "Газ", 1, 49, null },
                    { 51, null, "Бензин", 1, 49, null },
                    { 28, null, "Передні", 1, 27, null },
                    { 29, null, "Задні", 1, 27, null },
                    { 31, null, "Зимові", 1, 30, null },
                    { 32, null, "Літні", 1, 30, null },
                    { 34, null, "Омивайка", 1, 33, null },
                    { 35, null, "Олія моторна", 1, 33, null },
                    { 36, null, "Олія коробки передач", 1, 33, null },
                    { 38, null, "Салон", 1, 37, null },
                    { 39, null, "Фільтр олії (двигун)", 1, 37, null },
                    { 40, null, "Повітряний фільтр", 1, 37, null },
                    { 44, null, "Заміна тормозних колодок", 1, 43, null },
                    { 45, null, "Заміна масла двигуна", 1, 43, null },
                    { 46, null, "Заміна фільтра олії (двигун)", 1, 43, null },
                    { 47, null, "Заміна зовнішнього повітряного фільтра", 1, 43, null },
                    { 48, null, "ГБО", 1, 43, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrencyId",
                table: "Accounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Name_OwnerId_CurrencyId",
                table: "Accounts",
                columns: new[] { "Name", "OwnerId", "CurrencyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OwnerId",
                table: "Accounts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_BankId_IBAN",
                table: "BankAccounts",
                columns: new[] { "BankId", "IBAN" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Banks_Name",
                table: "Banks",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardAccounts_BankId",
                table: "CardAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_CardAccounts_CardNumber_BankId",
                table: "CardAccounts",
                columns: new[] { "CardNumber", "BankId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CountryCurrency_CurrenciesId",
                table: "CountryCurrency",
                column: "CurrenciesId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Name",
                table: "Currencies",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategories_Name_ParentId_OwnerId",
                table: "TransactionCategories",
                columns: new[] { "Name", "ParentId", "OwnerId" });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategories_OwnerId",
                table: "TransactionCategories",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionCategories_ParentId",
                table: "TransactionCategories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ActorId",
                table: "Transactions",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CorrelatedTransactionId",
                table: "Transactions",
                column: "CorrelatedTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "CardAccounts");

            migrationBuilder.DropTable(
                name: "CashAccounts");

            migrationBuilder.DropTable(
                name: "CountryCurrency");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "TransactionCategories");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
