using Microsoft.EntityFrameworkCore.Migrations;

namespace GameReviews.Data.Migrations
{
    public partial class AddIdentityUserToReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_CommentRefUserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_ReviewRefUserId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_ReviewRefUserId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Comment_CommentRefUserId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "CommentRefUserId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewRefUserId",
                table: "Review",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Review",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_UserId",
                table: "Review",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_UserId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_UserId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Review");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewRefUserId",
                table: "Review",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentRefUserId",
                table: "Comment",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ReviewRefUserId",
                table: "Review",
                column: "ReviewRefUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CommentRefUserId",
                table: "Comment",
                column: "CommentRefUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_CommentRefUserId",
                table: "Comment",
                column: "CommentRefUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_ReviewRefUserId",
                table: "Review",
                column: "ReviewRefUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
