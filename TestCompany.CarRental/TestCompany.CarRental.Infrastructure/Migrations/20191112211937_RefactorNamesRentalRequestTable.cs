using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCompany.CarRental.Infrastructure.Migrations
{
    public partial class RefactorNamesRentalRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RentalRequest",
                table: "RentalRequest");

            migrationBuilder.RenameTable(
                name: "RentalRequest",
                newName: "RentRequest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentRequest",
                table: "RentRequest",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RentRequest",
                table: "RentRequest");

            migrationBuilder.RenameTable(
                name: "RentRequest",
                newName: "RentalRequest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RentalRequest",
                table: "RentalRequest",
                column: "Id");
        }
    }
}
