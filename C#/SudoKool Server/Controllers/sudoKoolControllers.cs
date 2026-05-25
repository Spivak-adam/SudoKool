using Sudokool.Models;
using Sudokool.Services;
using Microsoft.AspNetCore.Mvc;

namespace Sudokool.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SudokoolController: ControllerBase
{
    public SudokoolController()
    {
        
    }

    [HttpGet("boards/{id}")]
    public ActionResult <List<Board>> get(int id) {
        SudokoolService.CreateBoard(id);
        
        List<Board> boards = SudokoolService.GetBoards();

        return Ok(boards);
        
    }
}