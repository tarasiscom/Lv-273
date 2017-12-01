using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EPA.MSSQL.Migrations
{
    public partial class addTableLogo_Universities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Logo_Universities");

            migrationBuilder.AddColumn<byte[]>(
                name: "Logo",
                table: "Logo_Universities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Logo_Universities");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Logo_Universities",
                nullable: true);
        }
    }
}
