using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReservationsAndRooms3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_roomId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "roomId",
                table: "Reservations",
                newName: "RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_roomId",
                table: "Reservations",
                newName: "IX_Reservations_RoomId");

            migrationBuilder.AddColumn<int>(
                name: "RId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Reservations",
                newName: "roomId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                newName: "IX_Reservations_roomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_roomId",
                table: "Reservations",
                column: "roomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
