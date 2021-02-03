using Microsoft.EntityFrameworkCore.Migrations;

namespace EPOSProjektni.Migrations.ApplicationDb
{
    public partial class DodavanjeSlik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NazivSlike",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NazivSlike",
                table: "Books");
        }
    }
}
