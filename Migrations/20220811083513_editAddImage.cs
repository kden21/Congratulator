using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Congratulator.Migrations
{
    public partial class editAddImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceImage",
                table: "Persons");

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "Persons",
                type: "bytea",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Persons");

            migrationBuilder.AddColumn<string>(
                name: "SourceImage",
                table: "Persons",
                type: "text",
                nullable: true);
        }
    }
}
