using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace biznus_web.Migrations
{
    /// <inheritdoc />
    public partial class AddProductTranslations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 20, 39, 19, 632, DateTimeKind.Local).AddTicks(1935));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 20, 39, 19, 632, DateTimeKind.Local).AddTicks(1965));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 20, 39, 19, 632, DateTimeKind.Local).AddTicks(1967));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 20, 39, 19, 632, DateTimeKind.Local).AddTicks(1969));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 20, 39, 19, 632, DateTimeKind.Local).AddTicks(1971));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 20, 39, 19, 632, DateTimeKind.Local).AddTicks(1973));

            migrationBuilder.InsertData(
                table: "Translations",
                columns: new[] { "Id", "Culture", "Key", "Scope", "UpdatedAt", "Value" },
                values: new object[,]
                {
                    { 1, "en-US", "Product_1_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "White Tent" },
                    { 2, "ru-RU", "Product_1_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Белая палатка" },
                    { 3, "kk-KZ", "Product_1_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Ақ шатыр" },
                    { 4, "fr-FR", "Product_1_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Tente blanche" },
                    { 5, "en-US", "Product_1_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "A reliable tent for your outdoor adventures" },
                    { 6, "ru-RU", "Product_1_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Надёжная палатка для ваших приключений на природе" },
                    { 7, "kk-KZ", "Product_1_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Табиғаттағы шытырман оқиғаларға арналған сенімді шатыр" },
                    { 8, "fr-FR", "Product_1_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Une tente fiable pour vos aventures en plein air" },
                    { 9, "en-US", "Product_2_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Tin Coffee Tumbler" },
                    { 10, "ru-RU", "Product_2_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Жестяная кофейная кружка" },
                    { 11, "kk-KZ", "Product_2_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Қалайы кофе стаканы" },
                    { 12, "fr-FR", "Product_2_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Tasse à café en étain" },
                    { 13, "en-US", "Product_2_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Keep your drinks hot or cold with this tumbler" },
                    { 14, "ru-RU", "Product_2_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Сохраняйте напитки горячими или холодными с этой кружкой" },
                    { 15, "kk-KZ", "Product_2_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Бұл стаканмен сусындарды ыстық немесе суық сақтаңыз" },
                    { 16, "fr-FR", "Product_2_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Gardez vos boissons chaudes ou froides avec cette tasse" },
                    { 17, "en-US", "Product_3_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Blue Canvas Pack" },
                    { 18, "ru-RU", "Product_3_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Синий холщовый рюкзак" },
                    { 19, "kk-KZ", "Product_3_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Көк кенеп рюкзак" },
                    { 20, "fr-FR", "Product_3_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Sac en toile bleue" },
                    { 21, "en-US", "Product_3_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Durable canvas pack for hiking and travel" },
                    { 22, "ru-RU", "Product_3_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Прочный холщовый рюкзак для походов и путешествий" },
                    { 23, "kk-KZ", "Product_3_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Жүру және саяхат үшін берік кенеп рюкзак" },
                    { 24, "fr-FR", "Product_3_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Sac en toile durable pour la randonnée et les voyages" },
                    { 25, "en-US", "Product_4_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Gift Card" },
                    { 26, "ru-RU", "Product_4_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Подарочная карта" },
                    { 27, "kk-KZ", "Product_4_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Сыйлық картасы" },
                    { 28, "fr-FR", "Product_4_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Carte cadeau" },
                    { 29, "en-US", "Product_4_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Perfect gift for any outdoor enthusiast" },
                    { 30, "ru-RU", "Product_4_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Идеальный подарок для любого любителя активного отдыха" },
                    { 31, "kk-KZ", "Product_4_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Кез келген табиғат сүйгішіне арналған мінсіз сыйлық" },
                    { 32, "fr-FR", "Product_4_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Cadeau parfait pour tout amateur de plein air" },
                    { 33, "en-US", "Product_5_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Green Canvas Pack" },
                    { 34, "ru-RU", "Product_5_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Зелёный холщовый рюкзак" },
                    { 35, "kk-KZ", "Product_5_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Жасыл кенеп рюкзак" },
                    { 36, "fr-FR", "Product_5_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Sac en toile verte" },
                    { 37, "en-US", "Product_5_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Stylish green canvas pack for adventures" },
                    { 38, "ru-RU", "Product_5_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Стильный зелёный холщовый рюкзак для приключений" },
                    { 39, "kk-KZ", "Product_5_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Шытырман оқиғаларға арналған стильді жасыл кенеп рюкзак" },
                    { 40, "fr-FR", "Product_5_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Sac en toile verte élégant pour les aventures" },
                    { 41, "en-US", "Product_6_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Red Tent" },
                    { 42, "ru-RU", "Product_6_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Красная палатка" },
                    { 43, "kk-KZ", "Product_6_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Қызыл шатыр" },
                    { 44, "fr-FR", "Product_6_Name", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Tente rouge" },
                    { 45, "en-US", "Product_6_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Spacious family tent for camping" },
                    { 46, "ru-RU", "Product_6_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Просторная семейная палатка для кемпинга" },
                    { 47, "kk-KZ", "Product_6_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Кемпинг үшін кең отбасылық шатыр" },
                    { 48, "fr-FR", "Product_6_Description", "Product", new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988), "Tente familiale spacieuse pour le camping" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 20, 8, 2, 790, DateTimeKind.Local).AddTicks(1674));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 20, 8, 2, 790, DateTimeKind.Local).AddTicks(1704));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 20, 8, 2, 790, DateTimeKind.Local).AddTicks(1705));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 20, 8, 2, 790, DateTimeKind.Local).AddTicks(1707));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 20, 8, 2, 790, DateTimeKind.Local).AddTicks(1709));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 8, 20, 8, 2, 790, DateTimeKind.Local).AddTicks(1765));
        }
    }
}
