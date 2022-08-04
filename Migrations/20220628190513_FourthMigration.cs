using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideOut.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Joins_RideOuts_RideOutId",
                table: "Joins");

            migrationBuilder.RenameColumn(
                name: "RideOutId",
                table: "RideOuts",
                newName: "RideId");

            migrationBuilder.RenameColumn(
                name: "RideOutId",
                table: "Joins",
                newName: "RideId");

            migrationBuilder.RenameIndex(
                name: "IX_Joins_RideOutId",
                table: "Joins",
                newName: "IX_Joins_RideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Joins_RideOuts_RideId",
                table: "Joins",
                column: "RideId",
                principalTable: "RideOuts",
                principalColumn: "RideId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Joins_RideOuts_RideId",
                table: "Joins");

            migrationBuilder.RenameColumn(
                name: "RideId",
                table: "RideOuts",
                newName: "RideOutId");

            migrationBuilder.RenameColumn(
                name: "RideId",
                table: "Joins",
                newName: "RideOutId");

            migrationBuilder.RenameIndex(
                name: "IX_Joins_RideId",
                table: "Joins",
                newName: "IX_Joins_RideOutId");

            migrationBuilder.AddForeignKey(
                name: "FK_Joins_RideOuts_RideOutId",
                table: "Joins",
                column: "RideOutId",
                principalTable: "RideOuts",
                principalColumn: "RideOutId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
