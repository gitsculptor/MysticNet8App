using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MysticNet8App.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatingroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6749a1ac-45e8-4479-82a8-bc8b3ae034f9", null, "Administrator", "ADMINISTRATOR" },
                    { "ac957e16-660a-4823-bb66-1f399b8df40d", null, "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6749a1ac-45e8-4479-82a8-bc8b3ae034f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac957e16-660a-4823-bb66-1f399b8df40d");
        }
    }
}
