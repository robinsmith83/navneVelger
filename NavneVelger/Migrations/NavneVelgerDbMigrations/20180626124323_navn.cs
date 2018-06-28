using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NavneVelger.Migrations.NavneVelgerDbMigrations
{
    public partial class navn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Navnene",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Gender = table.Column<int>(nullable: false),
                    Navnet = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Navnene", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NavnRangeringer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NavnId = table.Column<int>(nullable: true),
                    Rangering = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavnRangeringer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NavnRangeringer_Navnene_NavnId",
                        column: x => x.NavnId,
                        principalTable: "Navnene",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NavnRangeringer_NavnId",
                table: "NavnRangeringer",
                column: "NavnId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NavnRangeringer");

            migrationBuilder.DropTable(
                name: "Navnene");
        }
    }
}
