using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCompany.CarRental.Infrastructure.Migrations
{
    public partial class AddedForeignKeyCompanyToRentalRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RentRequest_CompanyId",
                table: "RentRequest",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentRequest_Company_CompanyId",
                table: "RentRequest",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentRequest_Company_CompanyId",
                table: "RentRequest");

            migrationBuilder.DropIndex(
                name: "IX_RentRequest_CompanyId",
                table: "RentRequest");
        }
    }
}
