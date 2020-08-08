using Microsoft.EntityFrameworkCore.Migrations;

namespace EMAProject.Migrations
{
    public partial class SessionModelFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_SessionNote_SessionNoteID",
                table: "Session");

            migrationBuilder.AlterColumn<int>(
                name: "SessionNoteID",
                table: "Session",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_SessionNote_SessionNoteID",
                table: "Session",
                column: "SessionNoteID",
                principalTable: "SessionNote",
                principalColumn: "SessionNoteID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_SessionNote_SessionNoteID",
                table: "Session");

            migrationBuilder.AlterColumn<int>(
                name: "SessionNoteID",
                table: "Session",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Session_SessionNote_SessionNoteID",
                table: "Session",
                column: "SessionNoteID",
                principalTable: "SessionNote",
                principalColumn: "SessionNoteID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
