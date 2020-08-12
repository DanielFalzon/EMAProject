using Microsoft.EntityFrameworkCore.Migrations;

namespace EMAProject.Migrations
{
    public partial class AddedIsReadtoWebView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PoliciesRead",
                table: "WebView",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PoliciesRead",
                table: "WebView");
        }
    }
}
