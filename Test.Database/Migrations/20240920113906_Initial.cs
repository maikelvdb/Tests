using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Test.Database.Migrations;

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
                NUllableBool = table.Column<bool>(type: "bit", nullable: true),
                Type = table.Column<int>(type: "int", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TestItems", x => x.Id);
            });

        migrationBuilder.InsertData(
            table: "TestItems",
            columns: new[] { "Id", "CreatedAt", "NUllableBool", "Name", "Type" },
            values: new object[,]
            {
                { 1, new DateTime(2024, 9, 20, 11, 39, 5, 407, DateTimeKind.Utc).AddTicks(1402), null, "Test 1", 0 },
                { 2, new DateTime(2024, 9, 20, 11, 39, 5, 407, DateTimeKind.Utc).AddTicks(1407), null, "Test 2", 0 },
                { 3, new DateTime(2024, 9, 20, 11, 39, 5, 407, DateTimeKind.Utc).AddTicks(1408), null, "Test 3", 0 },
                { 4, new DateTime(2024, 9, 20, 11, 39, 5, 407, DateTimeKind.Utc).AddTicks(1409), null, "Test 4", 0 },
                { 5, new DateTime(2024, 9, 20, 11, 39, 5, 407, DateTimeKind.Utc).AddTicks(1410), null, "Test 5", 0 }
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
