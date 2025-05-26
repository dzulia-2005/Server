using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class RemakeCreateUserById : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "803865da-83f0-48ac-a923-58a280112cf0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bab40cc5-ecb4-46d5-9b02-b03abed7acf0");

            migrationBuilder.RenameColumn(
                name: "CreatedByUserById",
                table: "Stocks",
                newName: "CreatedUserById");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11acd349-f53a-4913-a8b1-be39d016dccb", null, "Admin", "ADMIN" },
                    { "b33be947-bf1a-4cc5-98fa-5bc7a7fb8661", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11acd349-f53a-4913-a8b1-be39d016dccb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b33be947-bf1a-4cc5-98fa-5bc7a7fb8661");

            migrationBuilder.RenameColumn(
                name: "CreatedUserById",
                table: "Stocks",
                newName: "CreatedByUserById");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "803865da-83f0-48ac-a923-58a280112cf0", null, "Admin", "ADMIN" },
                    { "bab40cc5-ecb4-46d5-9b02-b03abed7acf0", null, "User", "USER" }
                });
        }
    }
}
