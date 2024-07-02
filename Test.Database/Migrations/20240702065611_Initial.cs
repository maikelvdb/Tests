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
                    Type = table.Column<int>(type: "int", nullable: false, defaultValue: 2),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TestItems",
                columns: new[] { "Id", "CreatedAt", "Name", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 7, 2, 6, 56, 10, 766, DateTimeKind.Utc).AddTicks(2851), "Test 1", 0 },
                    { 2, new DateTime(2024, 7, 2, 6, 56, 10, 766, DateTimeKind.Utc).AddTicks(2857), "Test 2", 0 },
                    { 3, new DateTime(2024, 7, 2, 6, 56, 10, 766, DateTimeKind.Utc).AddTicks(2859), "Test 3", 0 },
                    { 4, new DateTime(2024, 7, 2, 6, 56, 10, 766, DateTimeKind.Utc).AddTicks(2861), "Test 4", 0 },
                    { 5, new DateTime(2024, 7, 2, 6, 56, 10, 766, DateTimeKind.Utc).AddTicks(2863), "Test 5", 0 }
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
