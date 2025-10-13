using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L1_Zvejyba.Migrations
{
    /// <inheritdoc />
    public partial class fishfinally : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Body_Cities_cityName",
                table: "Body");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Body",
                table: "Body");

            migrationBuilder.RenameTable(
                name: "Body",
                newName: "Bodies");

            migrationBuilder.RenameIndex(
                name: "IX_Body_cityName",
                table: "Bodies",
                newName: "IX_Bodies_cityName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bodies",
                table: "Bodies",
                column: "Name");

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
                name: "IX_Fish_bodyName",
                table: "Fish",
                column: "bodyName");

            migrationBuilder.AddForeignKey(
                name: "FK_Bodies_Cities_cityName",
                table: "Bodies",
                column: "cityName",
                principalTable: "Cities",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bodies_Cities_cityName",
                table: "Bodies");

            migrationBuilder.DropTable(
                name: "Fish");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bodies",
                table: "Bodies");

            migrationBuilder.RenameTable(
                name: "Bodies",
                newName: "Body");

            migrationBuilder.RenameIndex(
                name: "IX_Bodies_cityName",
                table: "Body",
                newName: "IX_Body_cityName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Body",
                table: "Body",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Body_Cities_cityName",
                table: "Body",
                column: "cityName",
                principalTable: "Cities",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
