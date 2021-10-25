using Microsoft.EntityFrameworkCore.Migrations;

namespace Restart.Migrations
{
    public partial class ChatUpgrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_AspNetUsers_UserReceiverId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_AspNetUsers_UserSenderId",
                table: "Chat");

            migrationBuilder.RenameColumn(
                name: "UserSenderId",
                table: "Chat",
                newName: "UserChatReceiverId");

            migrationBuilder.RenameColumn(
                name: "UserReceiverId",
                table: "Chat",
                newName: "UserChatOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_UserSenderId",
                table: "Chat",
                newName: "IX_Chat_UserChatReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_UserReceiverId",
                table: "Chat",
                newName: "IX_Chat_UserChatOwnerId");

            migrationBuilder.AddColumn<string>(
                name: "UserMessageOwnerId",
                table: "Message",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserMessageReceiverId",
                table: "Message",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UseChatrOwnerId",
                table: "Chat",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserMessageOwnerId",
                table: "Message",
                column: "UserMessageOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserMessageReceiverId",
                table: "Message",
                column: "UserMessageReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_AspNetUsers_UserChatOwnerId",
                table: "Chat",
                column: "UserChatOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_AspNetUsers_UserChatReceiverId",
                table: "Chat",
                column: "UserChatReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_UserMessageOwnerId",
                table: "Message",
                column: "UserMessageOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_AspNetUsers_UserMessageReceiverId",
                table: "Message",
                column: "UserMessageReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_AspNetUsers_UserChatOwnerId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_AspNetUsers_UserChatReceiverId",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_UserMessageOwnerId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_AspNetUsers_UserMessageReceiverId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_UserMessageOwnerId",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_UserMessageReceiverId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "UserMessageOwnerId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "UserMessageReceiverId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "UseChatrOwnerId",
                table: "Chat");

            migrationBuilder.RenameColumn(
                name: "UserChatReceiverId",
                table: "Chat",
                newName: "UserSenderId");

            migrationBuilder.RenameColumn(
                name: "UserChatOwnerId",
                table: "Chat",
                newName: "UserReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_UserChatReceiverId",
                table: "Chat",
                newName: "IX_Chat_UserSenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_UserChatOwnerId",
                table: "Chat",
                newName: "IX_Chat_UserReceiverId");

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
    }
}
