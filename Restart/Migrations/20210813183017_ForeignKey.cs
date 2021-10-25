using Microsoft.EntityFrameworkCore.Migrations;

namespace Restart.Migrations
{
    public partial class ForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserSenderId",
                table: "Chat",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "UserReceiverId",
                table: "Chat",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_UserReceiverId",
                table: "Chat",
                column: "UserReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_UserSenderId",
                table: "Chat",
                column: "UserSenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_AspNetUsers_UserReceiverId",
                table: "Chat",
                column: "UserReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_AspNetUsers_UserSenderId",
                table: "Chat",
                column: "UserSenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_AspNetUsers_UserReceiverId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_AspNetUsers_UserSenderId",
                table: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Chat_UserReceiverId",
                table: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Chat_UserSenderId",
                table: "Chat");

            migrationBuilder.AlterColumn<string>(
                name: "UserSenderId",
                table: "Chat",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "UserReceiverId",
                table: "Chat",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
