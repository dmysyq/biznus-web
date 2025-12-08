using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace biznus_web.Migrations
{
    /// <inheritdoc />
    public partial class AddContactMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 9, 0, 41, 14, 273, DateTimeKind.Local).AddTicks(1023));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 9, 0, 41, 14, 273, DateTimeKind.Local).AddTicks(1046));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 9, 0, 41, 14, 273, DateTimeKind.Local).AddTicks(1048));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 9, 0, 41, 14, 273, DateTimeKind.Local).AddTicks(1049));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 9, 0, 41, 14, 273, DateTimeKind.Local).AddTicks(1051));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 12, 9, 0, 41, 14, 273, DateTimeKind.Local).AddTicks(1053));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 7,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 8,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 9,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 10,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 11,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 12,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 13,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 14,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 15,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 16,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 17,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 18,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 19,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 20,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 21,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 22,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 23,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 24,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 25,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 26,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 27,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 28,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 29,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 30,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 31,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 32,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 33,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 34,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 35,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 36,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 37,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 38,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 39,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 40,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 41,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 42,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 43,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 44,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 45,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 46,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 47,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 48,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 19, 41, 14, 273, DateTimeKind.Utc).AddTicks(1064));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactMessages");

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

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 4,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 5,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 6,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 7,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 8,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 9,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 10,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 11,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 12,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 13,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 14,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 15,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 16,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 17,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 18,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 19,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 20,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 21,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 22,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 23,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 24,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 25,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 26,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 27,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 28,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 29,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 30,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 31,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 32,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 33,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 34,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 35,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 36,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 37,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 38,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 39,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 40,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 41,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 42,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 43,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 44,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 45,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 46,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 47,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));

            migrationBuilder.UpdateData(
                table: "Translations",
                keyColumn: "Id",
                keyValue: 48,
                column: "UpdatedAt",
                value: new DateTime(2025, 12, 8, 15, 39, 19, 632, DateTimeKind.Utc).AddTicks(1988));
        }
    }
}
