using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Congratulator.Migrations
{
    public partial class addSourceImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SourceImage",
                table: "Persons",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceImage",
                table: "Persons");
        }
    }
}
