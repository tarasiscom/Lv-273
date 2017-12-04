using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EPA.MSSQL.Migrations
{
    public partial class Score : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestResult",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TestDetailedInfoId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResult_Tests_TestDetailedInfoId",
                        column: x => x.TestDetailedInfoId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestResult_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TestScore",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GeneralDirectionId = table.Column<int>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    TestResultId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestScore_GeneralDirection_GeneralDirectionId",
                        column: x => x.GeneralDirectionId,
                        principalTable: "GeneralDirection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TestScore_TestResult_TestResultId",
                        column: x => x.TestResultId,
                        principalTable: "TestResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_TestDetailedInfoId",
                table: "TestResult",
                column: "TestDetailedInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_UserId",
                table: "TestResult",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestScore_GeneralDirectionId",
                table: "TestScore",
                column: "GeneralDirectionId");

            migrationBuilder.CreateIndex(
                name: "IX_TestScore_TestResultId",
                table: "TestScore",
                column: "TestResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestScore");

            migrationBuilder.DropTable(
                name: "TestResult");
        }
    }
}
