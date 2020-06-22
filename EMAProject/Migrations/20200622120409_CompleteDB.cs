using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMAProject.Migrations
{
    public partial class CompleteDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ViewName",
                table: "WebView",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NiNumber = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    Lastname = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    ContactNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    AddressLine3 = table.Column<string>(nullable: true),
                    ReferredBy = table.Column<string>(nullable: true),
                    Subscriber = table.Column<bool>(nullable: false),
                    ClientNotes = table.Column<string>(nullable: true),
                    Medications = table.Column<string>(nullable: true),
                    SoFirstName = table.Column<string>(nullable: true),
                    SoLastName = table.Column<string>(nullable: true),
                    SoRelationship = table.Column<string>(nullable: true),
                    SoContactNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "HealthCareProvider",
                columns: table => new
                {
                    HealthCareProviderID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ContactNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthCareProvider", x => x.HealthCareProviderID);
                });

            migrationBuilder.CreateTable(
                name: "Intervention",
                columns: table => new
                {
                    InterventionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreInterventionScore = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    PostInterventionScore = table.Column<decimal>(type: "decimal(5, 2)", nullable: false),
                    Antecedence = table.Column<string>(nullable: true),
                    Behaviours = table.Column<string>(nullable: true),
                    Consequence = table.Column<string>(nullable: true),
                    Treatment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intervention", x => x.InterventionID);
                });

            migrationBuilder.CreateTable(
                name: "SessionNote",
                columns: table => new
                {
                    SessionNoteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoteFile = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionNote", x => x.SessionNoteID);
                });

            migrationBuilder.CreateTable(
                name: "ClientHealthCareProvider",
                columns: table => new
                {
                    HealthCareProviderID = table.Column<int>(nullable: false),
                    ClientID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientHealthCareProvider", x => new { x.ClientID, x.HealthCareProviderID });
                    table.ForeignKey(
                        name: "FK_ClientHealthCareProvider_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientHealthCareProvider_HealthCareProvider_HealthCareProviderID",
                        column: x => x.HealthCareProviderID,
                        principalTable: "HealthCareProvider",
                        principalColumn: "HealthCareProviderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientIntervention",
                columns: table => new
                {
                    ClientID = table.Column<int>(nullable: false),
                    InterventionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientIntervention", x => new { x.ClientID, x.InterventionID });
                    table.ForeignKey(
                        name: "FK_ClientIntervention_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientIntervention_Intervention_InterventionID",
                        column: x => x.InterventionID,
                        principalTable: "Intervention",
                        principalColumn: "InterventionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Session",
                columns: table => new
                {
                    SessionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionTime = table.Column<DateTime>(nullable: false),
                    IsAccompanied = table.Column<bool>(nullable: false),
                    IsDelivered = table.Column<bool>(nullable: false),
                    CancelledBy = table.Column<string>(nullable: false),
                    InterventionID = table.Column<int>(nullable: false),
                    SessionNoteID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session", x => x.SessionID);
                    table.ForeignKey(
                        name: "FK_Session_Intervention_InterventionID",
                        column: x => x.InterventionID,
                        principalTable: "Intervention",
                        principalColumn: "InterventionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Session_SessionNote_SessionNoteID",
                        column: x => x.SessionNoteID,
                        principalTable: "SessionNote",
                        principalColumn: "SessionNoteID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_Email",
                table: "Client",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Client_NiNumber",
                table: "Client",
                column: "NiNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientHealthCareProvider_HealthCareProviderID",
                table: "ClientHealthCareProvider",
                column: "HealthCareProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_ClientIntervention_InterventionID",
                table: "ClientIntervention",
                column: "InterventionID");

            migrationBuilder.CreateIndex(
                name: "IX_Session_InterventionID",
                table: "Session",
                column: "InterventionID");

            migrationBuilder.CreateIndex(
                name: "IX_Session_SessionNoteID",
                table: "Session",
                column: "SessionNoteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientHealthCareProvider");

            migrationBuilder.DropTable(
                name: "ClientIntervention");

            migrationBuilder.DropTable(
                name: "Session");

            migrationBuilder.DropTable(
                name: "HealthCareProvider");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Intervention");

            migrationBuilder.DropTable(
                name: "SessionNote");

            migrationBuilder.AlterColumn<string>(
                name: "ViewName",
                table: "WebView",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
