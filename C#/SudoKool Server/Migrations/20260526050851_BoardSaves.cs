using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SudoKool_Server.Migrations
{
    /// <inheritdoc />
    public partial class BoardSaves : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Games_gameID",
                table: "Boards");

            migrationBuilder.RenameColumn(
                name: "gameID",
                table: "Boards",
                newName: "GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_gameID",
                table: "Boards",
                newName: "IX_Boards_GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Games_GameId",
                table: "Boards",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Games_GameId",
                table: "Boards");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Boards",
                newName: "gameID");

            migrationBuilder.RenameIndex(
                name: "IX_Boards_GameId",
                table: "Boards",
                newName: "IX_Boards_gameID");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Games_gameID",
                table: "Boards",
                column: "gameID",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
