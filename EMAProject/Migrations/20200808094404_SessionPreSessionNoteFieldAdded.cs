using Microsoft.EntityFrameworkCore.Migrations;

namespace EMAProject.Migrations
{
    public partial class SessionPreSessionNoteFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreSessionNotes",
                table: "Session",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreSessionNotes",
                table: "Session");
        }
    }
}
