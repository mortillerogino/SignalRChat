using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalRChat.Migrations
{
    public partial class AddedUserToMessageRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "ChatMessage");

            migrationBuilder.AddColumn<int>(
                name: "ChatUserId",
                table: "ChatMessage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_ChatUserId",
                table: "ChatMessage",
                column: "ChatUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessage_ChatUser_ChatUserId",
                table: "ChatMessage",
                column: "ChatUserId",
                principalTable: "ChatUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessage_ChatUser_ChatUserId",
                table: "ChatMessage");

            migrationBuilder.DropIndex(
                name: "IX_ChatMessage_ChatUserId",
                table: "ChatMessage");

            migrationBuilder.DropColumn(
                name: "ChatUserId",
                table: "ChatMessage");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "ChatMessage",
                nullable: true);

        }
    }
}
