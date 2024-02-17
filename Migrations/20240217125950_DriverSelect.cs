using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transport.Migrations
{
    public partial class DriverSelect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Driver_LicenseCategoryId",
                table: "Driver",
                column: "LicenseCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Driver_LicenseCategory_LicenseCategoryId",
                table: "Driver",
                column: "LicenseCategoryId",
                principalTable: "LicenseCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Driver_LicenseCategory_LicenseCategoryId",
                table: "Driver");

            migrationBuilder.DropIndex(
                name: "IX_Driver_LicenseCategoryId",
                table: "Driver");
        }
    }
}
