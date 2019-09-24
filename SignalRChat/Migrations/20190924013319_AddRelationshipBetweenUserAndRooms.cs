using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SignalRChat.Migrations
{
    public partial class AddRelationshipBetweenUserAndRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatUserRoom",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ChatUserId = table.Column<int>(nullable: false),
                    ChatroomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUserRoom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatUserRoom_ChatUser_ChatUserId",
                        column: x => x.ChatUserId,
                        principalTable: "ChatUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatUserRoom_Chatroom_ChatroomId",
                        column: x => x.ChatroomId,
                        principalTable: "Chatroom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatUserRoom_ChatUserId",
                table: "ChatUserRoom",
                column: "ChatUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUserRoom_ChatroomId",
                table: "ChatUserRoom",
                column: "ChatroomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatUserRoom");
        }
    }
}
