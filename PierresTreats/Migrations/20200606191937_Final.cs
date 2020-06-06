using Microsoft.EntityFrameworkCore.Migrations;

namespace PierresTreats.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Treats");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Flavors");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Treats",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Flavors",
                nullable: true);
        }
    }
}
