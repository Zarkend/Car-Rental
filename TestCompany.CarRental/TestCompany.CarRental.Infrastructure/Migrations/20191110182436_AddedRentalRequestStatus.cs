using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCompany.CarRental.Infrastructure.Migrations
{
    public partial class AddedRentalRequestStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "RentalRequest",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "statusMessage",
                table: "RentalRequest",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "RentalRequest");

            migrationBuilder.DropColumn(
                name: "statusMessage",
                table: "RentalRequest");
        }
    }
}
