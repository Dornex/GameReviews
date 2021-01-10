using Microsoft.EntityFrameworkCore.Migrations;

namespace GameReviews.Data.Migrations
{
    public partial class AddFKToReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentRefReviewId",
                table: "Review");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentRefReviewId",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
