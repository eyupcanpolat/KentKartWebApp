using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KentKart.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddLostCardReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LostCardReports",
                columns: table => new
                {
                    LostCardReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Reason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Reported")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LostCardReports", x => x.LostCardReportId);
                    table.CheckConstraint("CK_LostCardReports_Status", "[Status] IN ('Reported', 'Reviewed', 'Rejected')");
                    table.ForeignKey(
                        name: "FK_LostCardReports_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LostCardReports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LostCardReports_CardId",
                table: "LostCardReports",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_LostCardReports_UserId",
                table: "LostCardReports",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LostCardReports");
        }
    }
}
