using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuestionEngine.Data.Migrations
{
    public partial class AddSurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SurveyId",
                table: "Questions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Survey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Survey", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions",
                column: "SurveyId");

            migrationBuilder.CreateIndex(
                name: "IX_Survey_Id",
                table: "Survey",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Survey_SurveyId",
                table: "Questions",
                column: "SurveyId",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Survey_SurveyId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "Survey");

            migrationBuilder.DropIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "SurveyId",
                table: "Questions");
        }
    }
}
