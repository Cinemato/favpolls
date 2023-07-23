using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Favpolls.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddVoteCountColumnToPollOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoteCount",
                table: "PollOptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoteCount",
                table: "PollOptions");
        }
    }
}
