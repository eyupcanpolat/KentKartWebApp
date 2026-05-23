using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KentKart.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CardTypeId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    RideCount = table.Column<int>(type: "int", nullable: false),
                    ValidityDays = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.SubscriptionPlanId);
                    table.CheckConstraint("CK_SubscriptionPlans_Price", "[Price] > 0");
                    table.CheckConstraint("CK_SubscriptionPlans_RideCount", "[RideCount] > 0");
                    table.CheckConstraint("CK_SubscriptionPlans_ValidityDays", "[ValidityDays] > 0");
                    table.ForeignKey(
                        name: "FK_SubscriptionPlans_CardTypes_CardTypeId",
                        column: x => x.CardTypeId,
                        principalTable: "CardTypes",
                        principalColumn: "CardTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CardSubscriptions",
                columns: table => new
                {
                    CardSubscriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemainingRideCount = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Active"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardSubscriptions", x => x.CardSubscriptionId);
                    table.CheckConstraint("CK_CardSubscriptions_DateRange", "[EndDate] >= [StartDate]");
                    table.CheckConstraint("CK_CardSubscriptions_RemainingRideCount", "[RemainingRideCount] >= 0");
                    table.CheckConstraint("CK_CardSubscriptions_Status", "[Status] IN ('Active', 'Expired', 'Cancelled')");
                    table.ForeignKey(
                        name: "FK_CardSubscriptions_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardSubscriptions_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "SubscriptionPlanId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "SubscriptionPlanId", "CardTypeId", "CreatedAt", "Description", "IsActive", "Name", "Price", "RideCount", "ValidityDays" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Öğrenci kartlar için 30 gün geçerli 100 binişlik abonman", true, "Ogrenci Aylik 100 Binis", 250m, 100, 30 },
                    { 2, 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tam kartlar için 30 gün geçerli 60 binişlik abonman", true, "Tam Aylik 60 Binis", 500m, 60, 30 },
                    { 3, 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "İndirimli kartlar için 30 gün geçerli 80 binişlik abonman", true, "Indirimli Aylik 80 Binis", 350m, 80, 30 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardSubscriptions_CardId",
                table: "CardSubscriptions",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CardSubscriptions_SubscriptionPlanId",
                table: "CardSubscriptions",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_CardTypeId",
                table: "SubscriptionPlans",
                column: "CardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_Name",
                table: "SubscriptionPlans",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardSubscriptions");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");
        }
    }
}
