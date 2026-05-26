using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SudoKool_Server.Migrations
{
    /// <inheritdoc />
    public partial class BoardUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "input",
                table: "Boards",
                newName: "Input");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Input",
                table: "Boards",
                newName: "input");
        }
    }
}
