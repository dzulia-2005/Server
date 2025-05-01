using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4cebbe51-0945-4923-98ca-0060f4542a10");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e22eb32b-0f36-4d6b-9c1e-d2b1013b665e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "df8daa05-3b46-4ec3-a742-c93ff8b3fd1b", null, "User", "USER" },
                    { "f5bc0757-6a4c-4d48-a50e-88e83e86c14c", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df8daa05-3b46-4ec3-a742-c93ff8b3fd1b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5bc0757-6a4c-4d48-a50e-88e83e86c14c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4cebbe51-0945-4923-98ca-0060f4542a10", null, "Admin", "ADMIN" },
                    { "e22eb32b-0f36-4d6b-9c1e-d2b1013b665e", null, "User", "USER" }
                });
        }
    }
}
