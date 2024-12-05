using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MP223_Terminkalender_v3.Migrations
{
    /// <inheritdoc />
    public partial class AddKeysToReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrivateKey",
                table: "Reservations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PublicKey",
                table: "Reservations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrivateKey",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PublicKey",
                table: "Reservations");
        }
    }
}
