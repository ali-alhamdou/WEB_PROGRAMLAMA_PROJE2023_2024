using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class shipadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    ShipID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShipStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    ShipEnd = table.Column<TimeSpan>(type: "time", nullable: false),
                    BreakStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    BreakEnd = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.ShipID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ships");
        }
    }
}
