namespace Sudokool.Models;

public class Board
{
    public int Id { get ; set; }
    public int GameID { get; set; }
    public int Quadrant { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public int input { get; set; }
    public DateTime DateEnter { get; set; }
}