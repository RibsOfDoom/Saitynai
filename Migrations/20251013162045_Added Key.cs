using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L1_Zvejyba.Migrations
{
    /// <inheritdoc />
    public partial class AddedKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CityName",
                table: "Bodies",
                newName: "cityName");

            migrationBuilder.AlterColumn<string>(
                name: "cityName",
                table: "Bodies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Bodies_cityName",
                table: "Bodies",
                column: "cityName");

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

            migrationBuilder.DropIndex(
                name: "IX_Bodies_cityName",
                table: "Bodies");

            migrationBuilder.RenameColumn(
                name: "cityName",
                table: "Bodies",
                newName: "CityName");

            migrationBuilder.AlterColumn<string>(
                name: "CityName",
                table: "Bodies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
