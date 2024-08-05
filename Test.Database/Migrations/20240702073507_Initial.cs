using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Test.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false, defaultValue: 3),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TestItems",
                columns: new[] { "Id", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 2, 7, 35, 7, 577, DateTimeKind.Utc).AddTicks(2800), "Test 1" },
                    { 2, new DateTime(2024, 7, 2, 7, 35, 7, 577, DateTimeKind.Utc).AddTicks(2802), "Test 2" },
                    { 3, new DateTime(2024, 7, 2, 7, 35, 7, 577, DateTimeKind.Utc).AddTicks(2803), "Test 3" },
                    { 4, new DateTime(2024, 7, 2, 7, 35, 7, 577, DateTimeKind.Utc).AddTicks(2804), "Test 4" },
                    { 5, new DateTime(2024, 7, 2, 7, 35, 7, 577, DateTimeKind.Utc).AddTicks(2805), "Test 5" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestItems_Id",
                table: "TestItems",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestItems");
        }
    }
}
