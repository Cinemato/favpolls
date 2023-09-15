using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Favpolls.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addPollSettingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PollSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VoteLimit = table.Column<int>(type: "int", nullable: true),
                    HideResults = table.Column<bool>(type: "bit", nullable: false),
                    HasCaptcha = table.Column<bool>(type: "bit", nullable: false),
                    PollId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollSetting_Polls_PollId",
                        column: x => x.PollId,
                        principalTable: "Polls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PollSetting_PollId",
                table: "PollSetting",
                column: "PollId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PollSetting");
        }
    }
}
