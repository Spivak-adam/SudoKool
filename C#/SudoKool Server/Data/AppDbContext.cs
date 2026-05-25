using Microsoft.EntityFrameworkCore;
using Sudokool.Models;

namespace Sudokool.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Board> Boards { get; set; }
}