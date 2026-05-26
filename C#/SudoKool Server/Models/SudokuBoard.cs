using System.Text.Json.Serialization;

namespace Sudokool.Models;

public class Board
{
    public int Id { get; set; }

    public int GameId { get; set; }

    [JsonIgnore]
    public Games Game { get; set; }

    public int Quadrant { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }

    public int Input { get; set; }

    public DateTime DateEnter { get; set; }
}