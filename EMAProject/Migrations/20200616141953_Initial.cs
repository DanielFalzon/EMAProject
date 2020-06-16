using Microsoft.EntityFrameworkCore.Migrations;

namespace EMAProject.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GdprPolicy",
                columns: table => new
                {
                    GdprPolicyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GdprPolicy", x => x.GdprPolicyID);
                });

            migrationBuilder.CreateTable(
                name: "WebView",
                columns: table => new
                {
                    WebViewID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViewName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebView", x => x.WebViewID);
                });

            migrationBuilder.CreateTable(
                name: "GdprPolicyWebView",
                columns: table => new
                {
                    GdprPolicyID = table.Column<int>(nullable: false),
                    WebViewID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GdprPolicyWebView", x => new { x.GdprPolicyID, x.WebViewID });
                    table.ForeignKey(
                        name: "FK_GdprPolicyWebView_GdprPolicy_GdprPolicyID",
                        column: x => x.GdprPolicyID,
                        principalTable: "GdprPolicy",
                        principalColumn: "GdprPolicyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GdprPolicyWebView_WebView_WebViewID",
                        column: x => x.WebViewID,
                        principalTable: "WebView",
                        principalColumn: "WebViewID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GdprPolicyWebView_WebViewID",
                table: "GdprPolicyWebView",
                column: "WebViewID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GdprPolicyWebView");

            migrationBuilder.DropTable(
                name: "GdprPolicy");

            migrationBuilder.DropTable(
                name: "WebView");
        }
    }
}
