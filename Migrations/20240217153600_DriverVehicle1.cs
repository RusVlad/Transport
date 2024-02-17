using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transport.Migrations
{
    public partial class DriverVehicle1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Driver",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Driver_VehicleId",
                table: "Driver",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Driver_Vehicle_VehicleId",
                table: "Driver",
                column: "VehicleId",
                principalTable: "Vehicle",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Driver_Vehicle_VehicleId",
                table: "Driver");

            migrationBuilder.DropIndex(
                name: "IX_Driver_VehicleId",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "Driver");
        }
    }
}
