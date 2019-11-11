using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCompany.CarRental.Infrastructure.Migrations
{
    public partial class AddedRentedUntilDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RentedUntilDate",
                table: "Car",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentedUntilDate",
                table: "Car");
        }
    }
}
