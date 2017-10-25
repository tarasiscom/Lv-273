using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EPA.DB.Migrations
{
    public partial class ForeignKey_Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id_direction",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "Id_university",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "Id_direction",
                table: "ProfDirection");

            migrationBuilder.DropColumn(
                name: "Id_test",
                table: "ProfDirection");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id_direction",
                table: "Specialties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id_university",
                table: "Specialties",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id_direction",
                table: "ProfDirection",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id_test",
                table: "ProfDirection",
                nullable: false,
                defaultValue: 0);
        }
    }
}
