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

    public async Task<List<Board>> StartGame()
    {
        Games game = new Games
        {
            DateStarted = DateTime.Now
        };

        _context.Games.Add(game);

        await _context.SaveChangesAsync();

        var boards = await GenerateBoard(game.Id, 7);

        return boards;
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
        List<Board> generatedBoards = new();

        int attempts = 0;

        while (generatedBoards.Count < cellsToGenerate && attempts < 1000)
        {
            attempts++;

            int row = rnd.Next(0, 9);
            int col = rnd.Next(0, 9);
            int input = rnd.Next(1, 10);
            int quad = GetQuadrant(row, col);

            bool spotTaken = generatedBoards.Any(b =>
                b.Row == row && b.Column == col
            );

            bool duplicateInGenerated = generatedBoards.Any(b =>
                b.Input == input &&
                (
                    b.Row == row ||
                    b.Column == col ||
                    b.Quadrant == quad
                )
            );

            bool duplicateInDb = await _context.Boards.AnyAsync(b =>
                b.GameId == gameId &&
                b.Input == input &&
                (
                    b.Row == row ||
                    b.Column == col ||
                    b.Quadrant == quad
                )
            );

            if (!spotTaken && !duplicateInGenerated && !duplicateInDb)
            {
                generatedBoards.Add(new Board
                {
                    GameId = gameId,
                    Row = row,
                    Column = col,
                    Quadrant = quad,
                    Input = input,
                    DateEnter = DateTime.Now
                });
            }
        }

        _context.Boards.AddRange(generatedBoards);
        await _context.SaveChangesAsync();

        return generatedBoards;
    }

    private int GetQuadrant(int row, int col)
    {
        return (row / 3) * 3 + (col / 3);
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
            .Where(b => b.GameId == board.GameId && b.Row == row)
            .ToListAsync();

        return !rowBoards.Any(b => newInput == b.Input);
    }

    public async Task<bool> ColValid(Board board, int col, int newInput)
    {
        var colBoards = await _context.Boards
            .Where(b => b.GameId == board.GameId && b.Column == col)
            .ToListAsync();

        return !colBoards.Any(b => newInput == b.Input);
    }

    public async Task<bool> QuadValid(Board board, int quad, int newInput)
    {
        var quadBoards = await _context.Boards
            .Where(b => b.GameId == board.GameId && b.Quadrant == quad)
            .ToListAsync();

        return !quadBoards.Any(b => newInput == b.Input);
    }

    public async Task<Board> SaveMove(Board board)
    {
        _context.Boards.Add(board);

        await _context.SaveChangesAsync();

        return board;
    }

}