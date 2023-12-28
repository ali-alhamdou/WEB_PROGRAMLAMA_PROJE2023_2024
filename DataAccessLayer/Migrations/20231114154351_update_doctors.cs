using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class update_doctors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Departments_DepartmentID",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Ships_ShipID",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "ShipID",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentID",
                table: "Doctors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Departments_DepartmentID",
                table: "Doctors",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Ships_ShipID",
                table: "Doctors",
                column: "ShipID",
                principalTable: "Ships",
                principalColumn: "ShipID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Departments_DepartmentID",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Ships_ShipID",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "ShipID",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentID",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Departments_DepartmentID",
                table: "Doctors",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Ships_ShipID",
                table: "Doctors",
                column: "ShipID",
                principalTable: "Ships",
                principalColumn: "ShipID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
