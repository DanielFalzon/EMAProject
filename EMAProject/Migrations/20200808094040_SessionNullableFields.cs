using Microsoft.EntityFrameworkCore.Migrations;

namespace EMAProject.Migrations
{
    public partial class SessionNullableFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_SessionNote_SessionNoteID",
                table: "Session");

            migrationBuilder.AlterColumn<int>(
                name: "SessionNoteID",
                table: "Session",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CancelledBy",
                table: "Session",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Session_SessionNote_SessionNoteID",
                table: "Session",
                column: "SessionNoteID",
                principalTable: "SessionNote",
                principalColumn: "SessionNoteID",
                onDelete: ReferentialAction.Restrict);
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
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CancelledBy",
                table: "Session",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_SessionNote_SessionNoteID",
                table: "Session",
                column: "SessionNoteID",
                principalTable: "SessionNote",
                principalColumn: "SessionNoteID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
