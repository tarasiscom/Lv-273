using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EPA.MSSQL.Migrations
{
    public partial class deleteprofdir : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfDirection");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
