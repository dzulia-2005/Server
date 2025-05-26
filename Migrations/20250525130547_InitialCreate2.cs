using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78d7125c-d8ac-476f-9c2e-0d289bbfcac8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c57ab5ae-9e64-4df8-b783-aa5aa3e7029f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "620c30b2-c0ed-4a64-8414-7bf47aa771a4", null, "User", "USER" },
                    { "e53283b8-19c3-4a94-96df-12ddabac08bd", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "620c30b2-c0ed-4a64-8414-7bf47aa771a4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e53283b8-19c3-4a94-96df-12ddabac08bd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "78d7125c-d8ac-476f-9c2e-0d289bbfcac8", null, "User", "USER" },
                    { "c57ab5ae-9e64-4df8-b783-aa5aa3e7029f", null, "Admin", "ADMIN" }
                });
        }
    }
}
