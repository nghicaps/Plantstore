using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Plantstore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LightLevels",
                columns: table => new
                {
                    LightLevelId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LightLevels", x => x.LightLevelId);
                });

            migrationBuilder.CreateTable(
                name: "ScientificNames",
                columns: table => new
                {
                    ScientificNameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Genus = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Species = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScientificNames", x => x.ScientificNameId);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    PlantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    LightLevelId = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.PlantId);
                    table.ForeignKey(
                        name: "FK_Plants_LightLevels_LightLevelId",
                        column: x => x.LightLevelId,
                        principalTable: "LightLevels",
                        principalColumn: "LightLevelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Scientifics",
                columns: table => new
                {
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    ScientificNameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scientifics", x => new { x.PlantId, x.ScientificNameId });
                    table.ForeignKey(
                        name: "FK_Scientifics_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "PlantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scientifics_ScientificNames_ScientificNameId",
                        column: x => x.ScientificNameId,
                        principalTable: "ScientificNames",
                        principalColumn: "ScientificNameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "LightLevels",
                columns: new[] { "LightLevelId", "Name" },
                values: new object[,]
                {
                    { "b", "Bright" },
                    { "l", "Low" },
                    { "m", "Medium" }
                });

            migrationBuilder.InsertData(
                table: "ScientificNames",
                columns: new[] { "ScientificNameId", "Genus", "Species" },
                values: new object[] { 1, "Monstera", "deliciosa" });

            migrationBuilder.InsertData(
                table: "Plants",
                columns: new[] { "PlantId", "LightLevelId", "Price", "Title" },
                values: new object[] { 1, "b", 5.0, "Swiss Cheese" });

            migrationBuilder.InsertData(
                table: "Scientifics",
                columns: new[] { "PlantId", "ScientificNameId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Plants_LightLevelId",
                table: "Plants",
                column: "LightLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Scientifics_ScientificNameId",
                table: "Scientifics",
                column: "ScientificNameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scientifics");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "ScientificNames");

            migrationBuilder.DropTable(
                name: "LightLevels");
        }
    }
}
