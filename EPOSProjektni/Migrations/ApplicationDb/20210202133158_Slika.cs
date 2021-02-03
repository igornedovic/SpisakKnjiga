using Microsoft.EntityFrameworkCore.Migrations;

namespace EPOSProjektni.Migrations.ApplicationDb
{
    public partial class Slika : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NazivSlike",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Slika",
                table: "Books",
                type: "nvarchar(50)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slika",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "NazivSlike",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
