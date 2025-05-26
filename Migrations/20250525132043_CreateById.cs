using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class CreateById : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "620c30b2-c0ed-4a64-8414-7bf47aa771a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e53283b8-19c3-4a94-96df-12ddabac08bd");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserById",
                table: "Stocks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "803865da-83f0-48ac-a923-58a280112cf0", null, "Admin", "ADMIN" },
                    { "bab40cc5-ecb4-46d5-9b02-b03abed7acf0", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "803865da-83f0-48ac-a923-58a280112cf0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bab40cc5-ecb4-46d5-9b02-b03abed7acf0");

            migrationBuilder.DropColumn(
                name: "CreatedByUserById",
                table: "Stocks");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "620c30b2-c0ed-4a64-8414-7bf47aa771a4", null, "User", "USER" },
                    { "e53283b8-19c3-4a94-96df-12ddabac08bd", null, "Admin", "ADMIN" }
                });
        }
    }
}
