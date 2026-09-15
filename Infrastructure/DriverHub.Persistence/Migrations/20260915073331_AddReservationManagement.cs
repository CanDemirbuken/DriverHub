using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriverHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "Reservations",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerFirstName",
                table: "Reservations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerLastName",
                table: "Reservations",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "Reservations",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProcessedAt",
                table: "Reservations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProcessedBy",
                table: "Reservations",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            // Historical values are unavailable; seed legacy snapshots from the current account.
            // Do not fabricate an approval/cancellation date or actor for old records.
            migrationBuilder.Sql("""
                UPDATE r SET r.CustomerFirstName = u.FirstName, r.CustomerLastName = u.LastName,
                    r.CustomerEmail = u.Email, r.CustomerPhone = LEFT(u.PhoneNumber, 50)
                FROM Reservations r INNER JOIN AspNetUsers u ON u.Id = r.UserId;
                """);

            migrationBuilder.CreateTable(
                name: "EmailOutboxMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Recipient = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextAttemptAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Attempts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailOutboxMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailOutboxMessages_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CreatedDate_Id",
                table: "Reservations",
                columns: new[] { "CreatedDate", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ProcessedBy",
                table: "Reservations",
                column: "ProcessedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Status_CreatedDate_Id",
                table: "Reservations",
                columns: new[] { "Status", "CreatedDate", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_EmailOutboxMessages_NextAttemptAt_CreatedAt",
                table: "EmailOutboxMessages",
                columns: new[] { "NextAttemptAt", "CreatedAt" },
                filter: "[DeliveredAt] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EmailOutboxMessages_ReservationId_Status",
                table: "EmailOutboxMessages",
                columns: new[] { "ReservationId", "Status" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_ProcessedBy",
                table: "Reservations",
                column: "ProcessedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_ProcessedBy",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "EmailOutboxMessages");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CreatedDate_Id",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ProcessedBy",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_Status_CreatedDate_Id",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CustomerFirstName",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CustomerLastName",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ProcessedAt",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ProcessedBy",
                table: "Reservations");
        }
    }
}
