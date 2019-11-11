using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCompany.CarRental.Infrastructure.Migrations
{
    public partial class NullableCompanyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Company_CompanyId",
                table: "Car");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Car",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Company_CompanyId",
                table: "Car",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Company_CompanyId",
                table: "Car");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Car",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Company_CompanyId",
                table: "Car",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
