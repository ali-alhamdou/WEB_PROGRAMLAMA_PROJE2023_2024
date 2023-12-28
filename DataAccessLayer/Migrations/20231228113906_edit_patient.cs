using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class edit_patient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mail",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Mail",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Doctors");
        }
    }
}
