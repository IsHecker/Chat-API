using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat_API.Migrations
{
    /// <inheritdoc />
    public partial class groupUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupAdmins_GroupConversations_GroupConversation1Id",
                table: "GroupAdmins");

            migrationBuilder.RenameColumn(
                name: "GroupConversation1Id",
                table: "GroupAdmins",
                newName: "GroupConversationId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupAdmins_GroupConversation1Id",
                table: "GroupAdmins",
                newName: "IX_GroupAdmins_GroupConversationId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupAdmins_GroupConversations_GroupConversationId",
                table: "GroupAdmins",
                column: "GroupConversationId",
                principalTable: "GroupConversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupAdmins_GroupConversations_GroupConversationId",
                table: "GroupAdmins");

            migrationBuilder.RenameColumn(
                name: "GroupConversationId",
                table: "GroupAdmins",
                newName: "GroupConversation1Id");

            migrationBuilder.RenameIndex(
                name: "IX_GroupAdmins_GroupConversationId",
                table: "GroupAdmins",
                newName: "IX_GroupAdmins_GroupConversation1Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupAdmins_GroupConversations_GroupConversation1Id",
                table: "GroupAdmins",
                column: "GroupConversation1Id",
                principalTable: "GroupConversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
