using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingBookingSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class IncludePrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Reservations",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "PricePerDay",
                table: "Parkings",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PricePerDay",
                table: "Parkings");
        }
    }
}
