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
                    { 1, new DateTime(2024, 3, 22, 10, 18, 0, 629, DateTimeKind.Utc).AddTicks(7817), "Test 1" },
                    { 2, new DateTime(2024, 3, 22, 10, 18, 0, 629, DateTimeKind.Utc).AddTicks(7818), "Test 2" },
                    { 3, new DateTime(2024, 3, 22, 10, 18, 0, 629, DateTimeKind.Utc).AddTicks(7819), "Test 3" },
                    { 4, new DateTime(2024, 3, 22, 10, 18, 0, 629, DateTimeKind.Utc).AddTicks(7821), "Test 4" },
                    { 5, new DateTime(2024, 3, 22, 10, 18, 0, 629, DateTimeKind.Utc).AddTicks(7822), "Test 5" }
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
