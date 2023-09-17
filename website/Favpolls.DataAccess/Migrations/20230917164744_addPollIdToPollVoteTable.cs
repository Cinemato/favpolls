using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Favpolls.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addPollIdToPollVoteTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PollId",
                table: "PollVotes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PollVotes_PollId",
                table: "PollVotes",
                column: "PollId");

            migrationBuilder.AddForeignKey(
                name: "FK_PollVotes_Polls_PollId",
                table: "PollVotes",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollVotes_Polls_PollId",
                table: "PollVotes");

            migrationBuilder.DropIndex(
                name: "IX_PollVotes_PollId",
                table: "PollVotes");

            migrationBuilder.DropColumn(
                name: "PollId",
                table: "PollVotes");
        }
    }
}
