using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSinavPortali.Migrations
{
    /// <inheritdoc />
    public partial class RelationExamAndResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResults_Exams_ExamID",
                table: "ExamResults");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_AspNetUsers_UserId",
                table: "StudentAnswers");

            migrationBuilder.DropIndex(
                name: "IX_StudentAnswers_UserId",
                table: "StudentAnswers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StudentAnswers");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "ExamResults");

            migrationBuilder.RenameColumn(
                name: "ExamID",
                table: "ExamResults",
                newName: "ExamId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamResults_ExamID",
                table: "ExamResults",
                newName: "IX_ExamResults_ExamId");

            migrationBuilder.AlterColumn<string>(
                name: "StudentID",
                table: "StudentAnswers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_StudentID",
                table: "StudentAnswers",
                column: "StudentID");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResults_Exams_ExamId",
                table: "ExamResults",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "ExamID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_AspNetUsers_StudentID",
                table: "StudentAnswers",
                column: "StudentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamResults_Exams_ExamId",
                table: "ExamResults");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_AspNetUsers_StudentID",
                table: "StudentAnswers");

            migrationBuilder.DropIndex(
                name: "IX_StudentAnswers_StudentID",
                table: "StudentAnswers");

            migrationBuilder.RenameColumn(
                name: "ExamId",
                table: "ExamResults",
                newName: "ExamID");

            migrationBuilder.RenameIndex(
                name: "IX_ExamResults_ExamId",
                table: "ExamResults",
                newName: "IX_ExamResults_ExamID");

            migrationBuilder.AlterColumn<string>(
                name: "StudentID",
                table: "StudentAnswers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "StudentAnswers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentID",
                table: "ExamResults",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_UserId",
                table: "StudentAnswers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamResults_Exams_ExamID",
                table: "ExamResults",
                column: "ExamID",
                principalTable: "Exams",
                principalColumn: "ExamID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_AspNetUsers_UserId",
                table: "StudentAnswers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
