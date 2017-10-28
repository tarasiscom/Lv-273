using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EPA.MSSQL.Migrations
{
    public partial class CorrectFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QestionID",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfDirection_Tests_TestDetailedInfoId",
                table: "ProfDirection");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Tests_TestListIDId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TestListIDId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_ProfDirection_TestDetailedInfoId",
                table: "ProfDirection");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QestionID",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "ApproximatedTime",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Question",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TestListIDId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TestDetailedInfoId",
                table: "ProfDirection");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QestionID",
                table: "Answers");

            migrationBuilder.AddColumn<int>(
                name: "ApproximateTime",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Questions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "ProfDirection",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuestionID",
                table: "Answers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestId",
                table: "Questions",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfDirection_TestId",
                table: "ProfDirection",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionID",
                table: "Answers",
                column: "QuestionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionID",
                table: "Answers",
                column: "QuestionID",
                principalTable: "Questions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfDirection_Tests_TestId",
                table: "ProfDirection",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Tests_TestId",
                table: "Questions",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionID",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfDirection_Tests_TestId",
                table: "ProfDirection");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Tests_TestId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TestId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_ProfDirection_TestId",
                table: "ProfDirection");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionID",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "ApproximateTime",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "ProfDirection");

            migrationBuilder.DropColumn(
                name: "QuestionID",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Answers");

            migrationBuilder.AddColumn<int>(
                name: "ApproximatedTime",
                table: "Tests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestListIDId",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestDetailedInfoId",
                table: "ProfDirection",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Answers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QestionID",
                table: "Answers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestListIDId",
                table: "Questions",
                column: "TestListIDId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfDirection_TestDetailedInfoId",
                table: "ProfDirection",
                column: "TestDetailedInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QestionID",
                table: "Answers",
                column: "QestionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QestionID",
                table: "Answers",
                column: "QestionID",
                principalTable: "Questions",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfDirection_Tests_TestDetailedInfoId",
                table: "ProfDirection",
                column: "TestDetailedInfoId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Tests_TestListIDId",
                table: "Questions",
                column: "TestListIDId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
