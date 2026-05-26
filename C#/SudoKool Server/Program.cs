using Microsoft.EntityFrameworkCore;
using Sudokool.Data;
using Sudokool.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<SudokoolService>();

var app = builder.Build();

app.MapControllers();

app.Run();