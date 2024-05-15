using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingBookingSystemAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangeParkingLatitude : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitute",
                table: "Parkings",
                newName: "Longitude");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Parkings",
                newName: "Longitute");
        }
    }
}
