using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NavneVelger.Migrations
{
    public partial class updateEierWithUserIdString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Boker");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Eiere",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Eiere");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Boker",
                nullable: true);
        }
    }
}
