using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCompany.CarRental.Infrastructure.Migrations
{
    public partial class AddedRentDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "RentalRequest",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Days",
                table: "RentalRequest");
        }
    }
}
