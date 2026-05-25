using Sudokool.Models;

namespace Sudokool.Services;

public static class SudokoolService
{
    static List<Board> Boards = new List<Board>();

    public static Board CreateBoard(int gameID)
    {
        Board board = new Board
        {
            GameID = gameID,
            Quadrant = 0,
            Row = 0,
            Column = 0,
            input = 0,
            DateEnter = DateTime.Now
        };

        Boards.Add(board);

        return board;
    }

    public static List<Board> GetBoards()
    {
        return Boards;
    }
}