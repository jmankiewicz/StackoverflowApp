using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackoverflowDb.Migrations
{
    /// <inheritdoc />
    public partial class SplitCommentsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Questions_QuestionId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_AuthorId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "QuestionComment");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_QuestionId",
                table: "QuestionComment",
                newName: "IX_QuestionComments_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_AuthorId",
                table: "QuestionComment",
                newName: "IX_QuestionComments_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionComments",
                table: "QuestionComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionComments_Questions_QuestionId",
                table: "QuestionComment",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionComments_Users_AuthorId",
                table: "QuestionComment",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionComments_Questions_QuestionId",
                table: "QuestionComment");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionComments_Users_AuthorId",
                table: "QuestionComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionComments",
                table: "QuestionComment");

            migrationBuilder.RenameTable(
                name: "QuestionComment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionComments_QuestionId",
                table: "Comments",
                newName: "IX_Comments_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionComments_AuthorId",
                table: "Comments",
                newName: "IX_Comments_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Questions_QuestionId",
                table: "Comments",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_AuthorId",
                table: "Comments",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
