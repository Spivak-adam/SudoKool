namespace Sudokool.Models;

public class Games
{
    public int Id { get ; set; }
    public DateTime DateStarted { get; set; }

    public List<Board> Boards { get; set;}
}