using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L1_Zvejyba.Migrations
{
    /// <inheritdoc />
    public partial class kkkkkk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Bodies",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cityName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bodies", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Bodies_Cities_cityName",
                        column: x => x.cityName,
                        principalTable: "Cities",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fish",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Season = table.Column<int>(type: "int", nullable: false),
                    TimeFrom = table.Column<int>(type: "int", nullable: false),
                    TimeTo = table.Column<int>(type: "int", nullable: false),
                    bodyName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fish", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fish_Bodies_bodyName",
                        column: x => x.bodyName,
                        principalTable: "Bodies",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bodies_cityName",
                table: "Bodies",
                column: "cityName");

            migrationBuilder.CreateIndex(
                name: "IX_Fish_bodyName",
                table: "Fish",
                column: "bodyName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fish");

            migrationBuilder.DropTable(
                name: "Bodies");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
