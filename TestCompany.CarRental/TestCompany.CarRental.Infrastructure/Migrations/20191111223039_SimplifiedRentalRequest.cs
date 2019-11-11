using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCompany.CarRental.Infrastructure.Migrations
{
    public partial class SimplifiedRentalRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "RentalRequest");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "RentalRequest");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "RentalRequest");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "RentalRequest",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarId",
                table: "RentalRequest");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "RentalRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Brand",
                table: "RentalRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "RentalRequest",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
