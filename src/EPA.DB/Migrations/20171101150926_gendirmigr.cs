using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EPA.MSSQL.Migrations
{
    public partial class gendirmigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeneralDirectionId",
                table: "Directions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GeneralDirection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralDirection", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Directions_GeneralDirectionId",
                table: "Directions",
                column: "GeneralDirectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Directions_GeneralDirection_GeneralDirectionId",
                table: "Directions",
                column: "GeneralDirectionId",
                principalTable: "GeneralDirection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Directions_GeneralDirection_GeneralDirectionId",
                table: "Directions");

            migrationBuilder.DropTable(
                name: "GeneralDirection");

            migrationBuilder.DropIndex(
                name: "IX_Directions_GeneralDirectionId",
                table: "Directions");

            migrationBuilder.DropColumn(
                name: "GeneralDirectionId",
                table: "Directions");
        }
    }
}
