using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class SpellSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Spells",
                columns: new[] { "Id", "Damage", "Name" },
                values: new object[,]
                {
                    { 1, 30, "Fireball" },
                    { 2, 25, "Blizzard" },
                    { 3, 35, "Thuderwave" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Spells",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Spells",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Spells",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
