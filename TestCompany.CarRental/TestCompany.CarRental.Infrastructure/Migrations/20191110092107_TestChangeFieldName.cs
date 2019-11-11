using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCompany.CarRental.Infrastructure.Migrations
{
    public partial class TestChangeFieldName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Model",
                table: "Car");

            migrationBuilder.AddColumn<string>(
                name: "Modela",
                table: "Car",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Modela",
                table: "Car");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Car",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
