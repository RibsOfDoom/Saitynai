using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace L1_Zvejyba.Migrations
{
    /// <inheritdoc />
    public partial class bodyNameRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bodyName",
                table: "Fish");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "bodyName",
                table: "Fish",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
