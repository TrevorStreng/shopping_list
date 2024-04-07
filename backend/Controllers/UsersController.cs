using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UsersController : ControllerBase
  {
    private readonly UserContext _context;

    public UsersController(UserContext context) {
      Console.WriteLine("herre");
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers() {
      var users = await _context.Users.ToListAsync();

      return Ok(users);
    } 

  }
}

