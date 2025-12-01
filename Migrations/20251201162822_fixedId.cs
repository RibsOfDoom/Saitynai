using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L1_Zvejyba.Migrations
{
    /// <inheritdoc />
    public partial class fixedId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bodies_Cities_cityName",
                table: "Bodies");

            migrationBuilder.DropForeignKey(
                name: "FK_Fish_Bodies_bodyName",
                table: "Fish");

            migrationBuilder.DropIndex(
                name: "IX_Fish_bodyName",
                table: "Fish");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bodies",
                table: "Bodies");

            migrationBuilder.DropIndex(
                name: "IX_Bodies_cityName",
                table: "Bodies");

            migrationBuilder.AlterColumn<string>(
                name: "bodyName",
                table: "Fish",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "bodyId",
                table: "Fish",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "cityName",
                table: "Bodies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Bodies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Bodies",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "cityId",
                table: "Bodies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bodies",
                table: "Bodies",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Fish_bodyId",
                table: "Fish",
                column: "bodyId");

            migrationBuilder.CreateIndex(
                name: "IX_Bodies_cityId",
                table: "Bodies",
                column: "cityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bodies_Cities_cityId",
                table: "Bodies",
                column: "cityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fish_Bodies_bodyId",
                table: "Fish",
                column: "bodyId",
                principalTable: "Bodies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bodies_Cities_cityId",
                table: "Bodies");

            migrationBuilder.DropForeignKey(
                name: "FK_Fish_Bodies_bodyId",
                table: "Fish");

            migrationBuilder.DropIndex(
                name: "IX_Fish_bodyId",
                table: "Fish");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bodies",
                table: "Bodies");

            migrationBuilder.DropIndex(
                name: "IX_Bodies_cityId",
                table: "Bodies");

            migrationBuilder.DropColumn(
                name: "bodyId",
                table: "Fish");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Bodies");

            migrationBuilder.DropColumn(
                name: "cityId",
                table: "Bodies");

            migrationBuilder.AlterColumn<string>(
                name: "bodyName",
                table: "Fish",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cities",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "cityName",
                table: "Bodies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Bodies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bodies",
                table: "Bodies",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Fish_bodyName",
                table: "Fish",
                column: "bodyName");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Fish_Bodies_bodyName",
                table: "Fish",
                column: "bodyName",
                principalTable: "Bodies",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
