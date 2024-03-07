using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MysticNet8App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addedfieldinusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0aeb9ac3-d435-4960-9184-6f7358eded18");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11351906-6609-4a0a-8ab1-bad07ee723d9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2f682426-791a-4e02-a881-9f55151067a6", null, "Administrator", "ADMINISTRATOR" },
                    { "f591f160-c64a-48a9-a660-c17d5a5fac2a", null, "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2f682426-791a-4e02-a881-9f55151067a6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f591f160-c64a-48a9-a660-c17d5a5fac2a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0aeb9ac3-d435-4960-9184-6f7358eded18", null, "Manager", "MANAGER" },
                    { "11351906-6609-4a0a-8ab1-bad07ee723d9", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
