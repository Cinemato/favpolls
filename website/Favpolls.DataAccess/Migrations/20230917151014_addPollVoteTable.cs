using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Favpolls.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addPollVoteTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollSetting_Polls_PollId",
                table: "PollSetting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PollSetting",
                table: "PollSetting");

            migrationBuilder.RenameTable(
                name: "PollSetting",
                newName: "PollSettings");

            migrationBuilder.RenameIndex(
                name: "IX_PollSetting_PollId",
                table: "PollSettings",
                newName: "IX_PollSettings_PollId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PollSettings",
                table: "PollSettings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PollVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PollOptionId = table.Column<int>(type: "int", nullable: false),
                    UserIP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollVotes_PollOptions_PollOptionId",
                        column: x => x.PollOptionId,
                        principalTable: "PollOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PollVotes_PollOptionId",
                table: "PollVotes",
                column: "PollOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PollSettings_Polls_PollId",
                table: "PollSettings",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollSettings_Polls_PollId",
                table: "PollSettings");

            migrationBuilder.DropTable(
                name: "PollVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PollSettings",
                table: "PollSettings");

            migrationBuilder.RenameTable(
                name: "PollSettings",
                newName: "PollSetting");

            migrationBuilder.RenameIndex(
                name: "IX_PollSettings_PollId",
                table: "PollSetting",
                newName: "IX_PollSetting_PollId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PollSetting",
                table: "PollSetting",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PollSetting_Polls_PollId",
                table: "PollSetting",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
