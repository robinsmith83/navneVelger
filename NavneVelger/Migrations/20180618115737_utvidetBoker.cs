using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace NavneVelger.Migrations
{
    public partial class utvidetBoker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boker_Eiere_EierId",
                table: "Boker");

            migrationBuilder.DropForeignKey(
                name: "FK_Boker_BokTyper_TypeId",
                table: "Boker");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Boker",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Navn",
                table: "Boker",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "EierId",
                table: "Boker",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TotaltAntallMerker",
                table: "Boker",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Boker_Eiere_EierId",
                table: "Boker",
                column: "EierId",
                principalTable: "Eiere",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Boker_BokTyper_TypeId",
                table: "Boker",
                column: "TypeId",
                principalTable: "BokTyper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boker_Eiere_EierId",
                table: "Boker");

            migrationBuilder.DropForeignKey(
                name: "FK_Boker_BokTyper_TypeId",
                table: "Boker");

            migrationBuilder.DropColumn(
                name: "TotaltAntallMerker",
                table: "Boker");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Boker",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Navn",
                table: "Boker",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EierId",
                table: "Boker",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Boker_Eiere_EierId",
                table: "Boker",
                column: "EierId",
                principalTable: "Eiere",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Boker_BokTyper_TypeId",
                table: "Boker",
                column: "TypeId",
                principalTable: "BokTyper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
