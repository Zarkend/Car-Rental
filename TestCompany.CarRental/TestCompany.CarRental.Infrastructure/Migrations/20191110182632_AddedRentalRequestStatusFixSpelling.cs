using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCompany.CarRental.Infrastructure.Migrations
{
    public partial class AddedRentalRequestStatusFixSpelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "statusMessage",
                table: "RentalRequest",
                newName: "StatusMessage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusMessage",
                table: "RentalRequest",
                newName: "statusMessage");
        }
    }
}
