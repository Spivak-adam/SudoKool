using Sudokool.Models;
using Sudokool.Data;
using Microsoft.EntityFrameworkCore;

namespace Sudokool.Services;

public class SudokoolService
{
    private readonly AppDbContext _context;

    public SudokoolService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Games>> GetGame()
    {
        return await _context.Games.ToListAsync();
    }

    public async Task<Games> CreateGame(Games game)
    {
        _context.Games.Add(game);

        await _context.SaveChangesAsync();

        return game;
    }

    public async Task<List<Board>> GetBoards(int gameId)
    {
        return await _context.Boards
            .Where(b => b.Game.Id == gameId)
            .ToListAsync();
    }

    public async Task<List<Board>> CreateStartingBoard(int gameId)
    {
        return await GenerateBoard(gameId, 20);
    }

    public async Task<List<Board>> GenerateBoard(int gameId, int cellsToGenerate)
    {
        Random rnd = new Random();
        List<Board> generatedBoards = new List<Board>();

        int attempts = 0;

        while (generatedBoards.Count < cellsToGenerate && attempts < 500)
        {
            attempts++;

            Board board = new Board
            {
                GameId = gameId,
                Quadrant = rnd.Next(0, 9),
                Row = rnd.Next(0, 9),
                Column = rnd.Next(0, 9),
                Input = rnd.Next(1, 10),
                DateEnter = DateTime.Now
            };

            bool moveValid = await CheckMove(
                board,
                board.Quadrant,
                board.Row,
                board.Column,
                board.Input
            );

            bool spotAlreadyUsed = generatedBoards.Any(b =>
                b.Row == board.Row &&
                b.Column == board.Column
            );

            if (moveValid && !spotAlreadyUsed)
            {
                generatedBoards.Add(board);
            }
        }

        _context.Boards.AddRange(generatedBoards);
        await _context.SaveChangesAsync();

        return generatedBoards;
    }

    public async Task<bool> CheckMove(Board board, int quad, int row, int col, int newInput)
    {
        bool quadValid = await QuadValid(board, quad, newInput);
        bool rowValid = await RowValid(board, row, newInput);
        bool colValid = await ColValid(board, col, newInput);

        return quadValid && rowValid && colValid;
    }

    public async Task<bool> RowValid(Board board, int row, int newInput)
    {
        var rowBoards = await _context.Boards
            .Where(b => b.Game.Id == board.Game.Id && b.Row == row)
            .ToListAsync();

        return !rowBoards.Any(b => newInput == b.Input);
    }

    public async Task<bool> ColValid(Board board, int col, int newInput)
    {
        var colBoards = await _context.Boards
            .Where(b => b.Game.Id == board.Game.Id && b.Column == col)
            .ToListAsync();

        return !colBoards.Any(b => newInput == b.Input);
    }

    public async Task<bool> QuadValid(Board board, int quad, int newInput)
    {
        var quadBoards = await _context.Boards
            .Where(b => b.Game.Id == board.Game.Id && b.Quadrant == quad)
            .ToListAsync();

        return !quadBoards.Any(b => newInput == b.Input);
    }


}