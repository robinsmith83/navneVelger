using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NavneVelger.Migrations
{
    public partial class boker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BokTyper",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BokTyper", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eiere",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Navn = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eiere", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boker",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aar = table.Column<int>(nullable: false),
                    EierId = table.Column<int>(nullable: false),
                    Navn = table.Column<string>(maxLength: 255, nullable: false),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boker_Eiere_EierId",
                        column: x => x.EierId,
                        principalTable: "Eiere",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Boker_BokTyper_TypeId",
                        column: x => x.TypeId,
                        principalTable: "BokTyper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Merker",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BokId = table.Column<int>(nullable: false),
                    Nummer = table.Column<int>(nullable: false),
                    klistretInn = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Merker_Boker_BokId",
                        column: x => x.BokId,
                        principalTable: "Boker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boker_EierId",
                table: "Boker",
                column: "EierId");

            migrationBuilder.CreateIndex(
                name: "IX_Boker_TypeId",
                table: "Boker",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Merker_BokId",
                table: "Merker",
                column: "BokId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Merker");

            migrationBuilder.DropTable(
                name: "Boker");

            migrationBuilder.DropTable(
                name: "Eiere");

            migrationBuilder.DropTable(
                name: "BokTyper");
        }
    }
}
