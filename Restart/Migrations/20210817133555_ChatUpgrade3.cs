using Microsoft.EntityFrameworkCore.Migrations;

namespace Restart.Migrations
{
    public partial class ChatUpgrade3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseChatOwnerId",
                table: "Chat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UseChatOwnerId",
                table: "Chat",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
