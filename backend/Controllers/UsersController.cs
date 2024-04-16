using System.Data;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UsersController : ControllerBase
  {
    private readonly IDbConnection _dbConnection;

    public UsersController(IDbConnection dbConnection) {
      _dbConnection = dbConnection;
    }

    // [HttpGet]
    // public async Task<IActionResult> GetAllUsers() {
    //   var users = await _dbConnection.;

    //   return Ok(users);
    // } 

  }

  // Entity framework might be the reason for slow queries
  // possibly try dapper
}

