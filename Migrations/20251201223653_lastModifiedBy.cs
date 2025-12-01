using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L1_Zvejyba.Migrations
{
    /// <inheritdoc />
    public partial class lastModifiedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "lastModifiedBy",
                table: "Fish",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lastModifiedBy",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lastModifiedBy",
                table: "Bodies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lastModifiedBy",
                table: "Fish");

            migrationBuilder.DropColumn(
                name: "lastModifiedBy",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "lastModifiedBy",
                table: "Bodies");
        }
    }
}
