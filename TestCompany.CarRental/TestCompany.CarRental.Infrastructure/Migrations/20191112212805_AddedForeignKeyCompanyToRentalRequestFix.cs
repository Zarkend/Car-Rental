using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCompany.CarRental.Infrastructure.Migrations
{
    public partial class AddedForeignKeyCompanyToRentalRequestFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentRequest_Company_CompanyId",
                table: "RentRequest");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "RentRequest",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RentRequest_Company_CompanyId",
                table: "RentRequest",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentRequest_Company_CompanyId",
                table: "RentRequest");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "RentRequest",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RentRequest_Company_CompanyId",
                table: "RentRequest",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
