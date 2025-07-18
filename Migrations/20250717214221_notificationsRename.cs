using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat_API.Migrations
{
    /// <inheritdoc />
    public partial class notificationsRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TargetId",
                table: "Notifications",
                newName: "SourceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SourceId",
                table: "Notifications",
                newName: "TargetId");
        }
    }
}
