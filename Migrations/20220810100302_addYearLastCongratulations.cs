using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Congratulator.Migrations
{
    public partial class addYearLastCongratulations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YearLastCongratulations",
                table: "Persons",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearLastCongratulations",
                table: "Persons");
        }
    }
}
