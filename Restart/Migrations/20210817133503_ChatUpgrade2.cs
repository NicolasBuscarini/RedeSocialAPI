using Microsoft.EntityFrameworkCore.Migrations;

namespace Restart.Migrations
{
    public partial class ChatUpgrade2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UseChatrOwnerId",
                table: "Chat",
                newName: "UseChatOwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UseChatOwnerId",
                table: "Chat",
                newName: "UseChatrOwnerId");
        }
    }
}
