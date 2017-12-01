using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EPA.MSSQL.Migrations
{
    public partial class addTableLogo_Universities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Logo_UniversitiesId",
                table: "Universities",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Logo_Universities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logo_Universities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Universities_Logo_UniversitiesId",
                table: "Universities",
                column: "Logo_UniversitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Universities_Logo_Universities_Logo_UniversitiesId",
                table: "Universities",
                column: "Logo_UniversitiesId",
                principalTable: "Logo_Universities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Universities_Logo_Universities_Logo_UniversitiesId",
                table: "Universities");

            migrationBuilder.DropTable(
                name: "Logo_Universities");

            migrationBuilder.DropIndex(
                name: "IX_Universities_Logo_UniversitiesId",
                table: "Universities");

            migrationBuilder.DropColumn(
                name: "Logo_UniversitiesId",
                table: "Universities");
        }
    }
}
