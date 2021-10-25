using Microsoft.EntityFrameworkCore.Migrations;

namespace Restart.Migrations
{
    public partial class LikePost3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Like",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Like",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Like_ApplicationUserId",
                table: "Like",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_PostId",
                table: "Like",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Like_AspNetUsers_ApplicationUserId",
                table: "Like",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Post_PostId",
                table: "Like",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Like_AspNetUsers_ApplicationUserId",
                table: "Like");

            migrationBuilder.DropForeignKey(
                name: "FK_Like_Post_PostId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_ApplicationUserId",
                table: "Like");

            migrationBuilder.DropIndex(
                name: "IX_Like_PostId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Like");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Like");
        }
    }
}
