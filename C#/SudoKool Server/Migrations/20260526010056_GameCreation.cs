using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SudoKool_Server.Migrations
{
    /// <inheritdoc />
    public partial class GameCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GameID",
                table: "Boards",
                newName: "gameID");

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateStarted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boards_gameID",
                table: "Boards",
                column: "gameID");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Games_gameID",
                table: "Boards",
                column: "gameID",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Games_gameID",
                table: "Boards");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Boards_gameID",
                table: "Boards");

            migrationBuilder.RenameColumn(
                name: "gameID",
                table: "Boards",
                newName: "GameID");
        }
    }
}
