using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EPA.MSSQL.Migrations
{
    public partial class addLByteLogoColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Universities_Logo_Universities_Logo_UniversitiesId",
                table: "Universities");

            migrationBuilder.DropIndex(
                name: "IX_Universities_Logo_UniversitiesId",
                table: "Universities");

            migrationBuilder.DropColumn(
                name: "Logo_UniversitiesId",
                table: "Universities");

            migrationBuilder.AddColumn<byte[]>(
                name: "Logo",
                table: "Universities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Universities");

            migrationBuilder.AddColumn<int>(
                name: "Logo_UniversitiesId",
                table: "Universities",
                nullable: true);

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
    }
}
