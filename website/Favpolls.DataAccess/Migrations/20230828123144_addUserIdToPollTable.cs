using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Favpolls.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addUserIdToPollTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Polls",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Polls_UserId",
                table: "Polls",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Polls_AspNetUsers_UserId",
                table: "Polls",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Polls_AspNetUsers_UserId",
                table: "Polls");

            migrationBuilder.DropIndex(
                name: "IX_Polls_UserId",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Polls");
        }
    }
}
