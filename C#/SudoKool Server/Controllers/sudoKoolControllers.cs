using Sudokool.Models;
using Sudokool.Services;
using Microsoft.AspNetCore.Mvc;

namespace Sudokool.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SudokoolController : ControllerBase
{
    private readonly SudokoolService _service;

    public SudokoolController(SudokoolService service)
    {
        _service = service;
    }

    [HttpGet("games")]
    public async Task<ActionResult<List<Games>>> GetGame()
    {
        var games = await _service.GetGame();

        return Ok(games);
    }

    [HttpPost("games")]
    public async Task<ActionResult<Games>> CreateGame(Games game)
    {
        var newGame = await _service.CreateGame(game);

        return Ok(newGame);
    }

    [HttpGet("games/{gameId}/boards")]
    public async Task<ActionResult<List<Board>>> GetBoards(int gameId)
    {
        var boards = await _service.GetBoards(gameId);

        return Ok(boards);
    }

    [HttpPost("start-game")]
    public async Task<ActionResult<List<Board>>> StartGame()
    {
        var boards = await _service.StartGame();

        return Ok(boards);
    }

    [HttpPost("boards")]
    public async Task<ActionResult<Board>> CreateBoard(int gameId)
    {
        var newBoard = await _service.CreateStartingBoard(gameId);

        return Ok(newBoard);
    }

    [HttpPost("move")]
public async Task<ActionResult<Board>> SaveMove(SaveMoveDto move)
{
    var savedMove = await _service.SaveMove(move);
    return Ok(savedMove);
}
}