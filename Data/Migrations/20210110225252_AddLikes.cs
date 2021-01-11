using Microsoft.EntityFrameworkCore.Migrations;

namespace GameReviews.Data.Migrations
{
    public partial class AddLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Review_ReviewId",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_ReviewId",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Comment");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewRefUserId",
                table: "Review",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CommentRefUserId",
                table: "Comment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ReviewId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.ReviewId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Likes_Review_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Review",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_ReviewRefUserId",
                table: "Review",
                column: "ReviewRefUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CommentRefReviewId",
                table: "Comment",
                column: "CommentRefReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CommentRefUserId",
                table: "Comment",
                column: "CommentRefUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_UserId",
                table: "Likes",
                column: "UserId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_CommentRefUserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_ReviewRefUserId",
                table: "Review");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Review_ReviewRefUserId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Comment_CommentRefReviewId",
                table: "Comment");

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
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "Comment",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ReviewId",
                table: "Comment",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Review_ReviewId",
                table: "Comment",
                column: "ReviewId",
                principalTable: "Review",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
