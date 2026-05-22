using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KentKart.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTripTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusLines",
                columns: table => new
                {
                    BusLineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LineCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusLines", x => x.BusLineId);
                });

            migrationBuilder.CreateTable(
                name: "FareRules",
                columns: table => new
                {
                    FareRuleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardTypeId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FareRules", x => x.FareRuleId);
                    table.CheckConstraint("CK_FareRules_DateRange", "[ValidTo] IS NULL OR [ValidTo] >= [ValidFrom]");
                    table.CheckConstraint("CK_FareRules_Price", "[Price] > 0");
                    table.ForeignKey(
                        name: "FK_FareRules_CardTypes_CardTypeId",
                        column: x => x.CardTypeId,
                        principalTable: "CardTypes",
                        principalColumn: "CardTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    District = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                });

            migrationBuilder.CreateTable(
                name: "LineStations",
                columns: table => new
                {
                    LineStationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusLineId = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    StationOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineStations", x => x.LineStationId);
                    table.CheckConstraint("CK_LineStations_StationOrder", "[StationOrder] > 0");
                    table.ForeignKey(
                        name: "FK_LineStations_BusLines_BusLineId",
                        column: x => x.BusLineId,
                        principalTable: "BusLines",
                        principalColumn: "BusLineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LineStations_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    BusLineId = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    FareAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TripDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Completed")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                    table.CheckConstraint("CK_Trips_FareAmount", "[FareAmount] >= 0");
                    table.CheckConstraint("CK_Trips_Status", "[Status] IN ('Completed', 'Failed', 'Cancelled')");
                    table.ForeignKey(
                        name: "FK_Trips_BusLines_BusLineId",
                        column: x => x.BusLineId,
                        principalTable: "BusLines",
                        principalColumn: "BusLineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BusLines",
                columns: new[] { "BusLineId", "CreatedAt", "Description", "IsActive", "LineCode", "LineName" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Umuttepe kampüsü ile İzmit Otogar arası hat", true, "41K", "Umuttepe - İzmit Otogar" },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "İzmit ve Gebze arası şehir içi ulaşım hattı", true, "200", "İzmit - Gebze" },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Körfez ve İzmit merkezi arası hat", true, "145", "Körfez - İzmit" }
                });

            migrationBuilder.InsertData(
                table: "FareRules",
                columns: new[] { "FareRuleId", "CardTypeId", "IsActive", "Price", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { 1, 1, true, 20m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 2, 2, true, 10m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 3, 3, true, 14m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "StationId", "CreatedAt", "District", "IsActive", "StationName" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "İzmit", true, "Umuttepe Kampüs" },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "İzmit", true, "Yahya Kaptan" },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "İzmit", true, "Anıtpark" },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "İzmit", true, "İzmit Otogar" },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gebze", true, "Gebze Merkez" },
                    { 6, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Körfez", true, "Körfez Merkez" }
                });

            migrationBuilder.InsertData(
                table: "LineStations",
                columns: new[] { "LineStationId", "BusLineId", "StationId", "StationOrder" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 1, 2, 2 },
                    { 3, 1, 3, 3 },
                    { 4, 1, 4, 4 },
                    { 5, 2, 4, 1 },
                    { 6, 2, 5, 2 },
                    { 7, 3, 6, 1 },
                    { 8, 3, 3, 2 },
                    { 9, 3, 4, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusLines_LineCode",
                table: "BusLines",
                column: "LineCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FareRules_CardTypeId",
                table: "FareRules",
                column: "CardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LineStations_BusLineId_StationId",
                table: "LineStations",
                columns: new[] { "BusLineId", "StationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LineStations_BusLineId_StationOrder",
                table: "LineStations",
                columns: new[] { "BusLineId", "StationOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LineStations_StationId",
                table: "LineStations",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_StationName",
                table: "Stations",
                column: "StationName");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_BusLineId_TripDate",
                table: "Trips",
                columns: new[] { "BusLineId", "TripDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CardId",
                table: "Trips",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_StationId",
                table: "Trips",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TripDate",
                table: "Trips",
                column: "TripDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FareRules");

            migrationBuilder.DropTable(
                name: "LineStations");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "BusLines");

            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
