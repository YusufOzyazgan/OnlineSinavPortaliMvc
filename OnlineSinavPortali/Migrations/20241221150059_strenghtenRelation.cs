using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSinavPortali.Migrations
{
    /// <inheritdoc />
    public partial class strenghtenRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_AspNetUsers_UserId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Exams_ExamID",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_AspNetUsers_StudentID",
                table: "StudentAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_Exams_ExamID",
                table: "StudentAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_Questions_QuestionID",
                table: "StudentAnswers");

            migrationBuilder.DropIndex(
                name: "IX_StudentAnswers_QuestionID",
                table: "StudentAnswers");

            migrationBuilder.DropIndex(
                name: "IX_Exams_UserId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Exams");

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "StudentAnswers",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "QuestionID",
                table: "StudentAnswers",
                newName: "QuestionId");

            migrationBuilder.RenameColumn(
                name: "ExamID",
                table: "StudentAnswers",
                newName: "ExamId");

            migrationBuilder.RenameColumn(
                name: "StudentAnswerID",
                table: "StudentAnswers",
                newName: "StudentAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAnswers_StudentID",
                table: "StudentAnswers",
                newName: "IX_StudentAnswers_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAnswers_ExamID",
                table: "StudentAnswers",
                newName: "IX_StudentAnswers_ExamId");

            migrationBuilder.RenameColumn(
                name: "ExamID",
                table: "Questions",
                newName: "ExamId");

            migrationBuilder.RenameColumn(
                name: "QuestionID",
                table: "Questions",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_ExamID",
                table: "Questions",
                newName: "IX_Questions_ExamId");

            migrationBuilder.RenameColumn(
                name: "TeacherID",
                table: "Exams",
                newName: "TeacherId");

            migrationBuilder.RenameColumn(
                name: "ExamID",
                table: "Exams",
                newName: "ExamId");

            migrationBuilder.RenameColumn(
                name: "ResultID",
                table: "ExamResults",
                newName: "ResultId");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherId",
                table: "Exams",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_QuestionId",
                table: "StudentAnswers",
                column: "QuestionId",
                unique: false);

            migrationBuilder.CreateIndex(
                name: "IX_Exams_TeacherId",
                table: "Exams",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_AspNetUsers_TeacherId",
                table: "Exams",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Exams_ExamId",
                table: "Questions",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "ExamId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_AspNetUsers_StudentId",
                table: "StudentAnswers",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_Exams_ExamId",
                table: "StudentAnswers",
                column: "ExamId",
                principalTable: "Exams",
                principalColumn: "ExamId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_Questions_QuestionId",
                table: "StudentAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "QuestionId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_AspNetUsers_TeacherId",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Exams_ExamId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_AspNetUsers_StudentId",
                table: "StudentAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_Exams_ExamId",
                table: "StudentAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAnswers_Questions_QuestionId",
                table: "StudentAnswers");

            migrationBuilder.DropIndex(
                name: "IX_StudentAnswers_QuestionId",
                table: "StudentAnswers");

            migrationBuilder.DropIndex(
                name: "IX_Exams_TeacherId",
                table: "Exams");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "StudentAnswers",
                newName: "StudentID");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "StudentAnswers",
                newName: "QuestionID");

            migrationBuilder.RenameColumn(
                name: "ExamId",
                table: "StudentAnswers",
                newName: "ExamID");

            migrationBuilder.RenameColumn(
                name: "StudentAnswerId",
                table: "StudentAnswers",
                newName: "StudentAnswerID");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAnswers_StudentId",
                table: "StudentAnswers",
                newName: "IX_StudentAnswers_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_StudentAnswers_ExamId",
                table: "StudentAnswers",
                newName: "IX_StudentAnswers_ExamID");

            migrationBuilder.RenameColumn(
                name: "ExamId",
                table: "Questions",
                newName: "ExamID");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "Questions",
                newName: "QuestionID");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_ExamId",
                table: "Questions",
                newName: "IX_Questions_ExamID");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Exams",
                newName: "TeacherID");

            migrationBuilder.RenameColumn(
                name: "ExamId",
                table: "Exams",
                newName: "ExamID");

            migrationBuilder.RenameColumn(
                name: "ResultId",
                table: "ExamResults",
                newName: "ResultID");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherID",
                table: "Exams",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Exams",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswers_QuestionID",
                table: "StudentAnswers",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Exams_UserId",
                table: "Exams",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_AspNetUsers_UserId",
                table: "Exams",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Exams_ExamID",
                table: "Questions",
                column: "ExamID",
                principalTable: "Exams",
                principalColumn: "ExamID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_AspNetUsers_StudentID",
                table: "StudentAnswers",
                column: "StudentID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_Exams_ExamID",
                table: "StudentAnswers",
                column: "ExamID",
                principalTable: "Exams",
                principalColumn: "ExamID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAnswers_Questions_QuestionID",
                table: "StudentAnswers",
                column: "QuestionID",
                principalTable: "Questions",
                principalColumn: "QuestionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
