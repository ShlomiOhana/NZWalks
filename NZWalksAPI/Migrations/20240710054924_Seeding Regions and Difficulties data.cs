using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalksAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedingRegionsandDifficultiesdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Walks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LengthInKm = table.Column<double>(type: "float", nullable: false),
                    WalkImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DifficultyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Walks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Walks_Difficulties_DifficultyId",
                        column: x => x.DifficultyId,
                        principalTable: "Difficulties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Walks_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("879a80ee-ebac-4d4a-855a-641fb5f48c8d"), "Easy" },
                    { new Guid("c88fb339-4ff0-45b7-befc-99daf87581ab"), "Medium" },
                    { new Guid("fcd7ddca-ea32-470f-bae5-c322fdc1490c"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("4c3c5e98-22e8-47fe-8b97-8843a0bc91a5"), "AKL", "Auckland", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fen.wikipedia.org%2Fwiki%2FAuckland&psig=AOvVaw2aBP2XnJ0K4ou_VGMhxGUx&ust=1720605944907000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCLCNxubamYcDFQAAAAAdAAAAABAE" },
                    { new Guid("75750007-efbd-46c2-8763-b193462e1390"), "NSN", "Nelson", "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.istockphoto.com%2Fphotos%2Fnelson-new-zealand&psig=AOvVaw0fJynlCYXwXyVVyR9zy6SD&ust=1720606022656000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCMj_3ojbmYcDFQAAAAAdAAAAABAE" },
                    { new Guid("7a2a6abe-62f6-42b2-950b-de1d6f461efa"), "STL", "Southland", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Walks_DifficultyId",
                table: "Walks",
                column: "DifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Walks_RegionId",
                table: "Walks",
                column: "RegionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Walks");

            migrationBuilder.DropTable(
                name: "Difficulties");

            migrationBuilder.DropTable(
                name: "Regions");
        }
    }
}
