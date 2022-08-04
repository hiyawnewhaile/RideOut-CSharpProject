using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideOut.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Joins_RideOuts_RideId",
                table: "Joins");

            migrationBuilder.DropForeignKey(
                name: "FK_RideOuts_Users_UserId",
                table: "RideOuts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RideOuts",
                table: "RideOuts");

            migrationBuilder.RenameTable(
                name: "RideOuts",
                newName: "Rides");

            migrationBuilder.RenameIndex(
                name: "IX_RideOuts_UserId",
                table: "Rides",
                newName: "IX_Rides_UserId");

            migrationBuilder.AddColumn<string>(
                name: "BikeType",
                table: "Bikes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Add1",
                table: "Rides",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Add2",
                table: "Rides",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BikeType",
                table: "Rides",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Rides",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Exclusive",
                table: "Rides",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Rides",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "Rides",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rides",
                table: "Rides",
                column: "RideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Joins_Rides_RideId",
                table: "Joins",
                column: "RideId",
                principalTable: "Rides",
                principalColumn: "RideId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Users_UserId",
                table: "Rides",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Joins_Rides_RideId",
                table: "Joins");

            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Users_UserId",
                table: "Rides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rides",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "BikeType",
                table: "Bikes");

            migrationBuilder.DropColumn(
                name: "Add1",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "Add2",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "BikeType",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "Exclusive",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Rides");

            migrationBuilder.RenameTable(
                name: "Rides",
                newName: "RideOuts");

            migrationBuilder.RenameIndex(
                name: "IX_Rides_UserId",
                table: "RideOuts",
                newName: "IX_RideOuts_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RideOuts",
                table: "RideOuts",
                column: "RideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Joins_RideOuts_RideId",
                table: "Joins",
                column: "RideId",
                principalTable: "RideOuts",
                principalColumn: "RideId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RideOuts_Users_UserId",
                table: "RideOuts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
