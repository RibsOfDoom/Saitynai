using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L1_Zvejyba.Migrations
{
    /// <inheritdoc />
    public partial class cityIdbodyId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cityName",
                table: "Bodies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cityName",
                table: "Bodies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
