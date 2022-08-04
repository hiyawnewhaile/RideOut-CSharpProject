using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RideOut.Migrations
{
    public partial class SeventhMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Add1",
                table: "Rides");

            migrationBuilder.RenameColumn(
                name: "Add2",
                table: "Rides",
                newName: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Rides",
                newName: "Add2");

            migrationBuilder.AddColumn<string>(
                name: "Add1",
                table: "Rides",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
