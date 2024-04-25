using System.Data;
using backend.Models;
using backend.Services;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UsersController : ControllerBase
  {
    private readonly IDbConnection _dbConnection;
    private readonly PasswordHashingService _passwordHashingService;

    public UsersController(IDbConnection dbConnection, PasswordHashingService passwordHashingService) {
      _dbConnection = dbConnection;
      _passwordHashingService = passwordHashingService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllUsers() {
      try {
        string query = "SELECT * FROM Users";
        var users = await _dbConnection.QueryAsync<User>(query);
        return Ok(users);
      } catch(Exception ex) {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetUser(int id) {
      try {
        string query = "SELECT * FROM Users WHERE Id = @Id;";
        var user = await _dbConnection.QueryAsync<User>(query, new{Id = id});
        return Ok(user);
      } catch(Exception ex) {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost]
    public async Task<ActionResult> CreateUser(User user) {
      // try {
        System.Console.WriteLine(user);
        string query = "INSERT INTO Users (username, password, email) values (@username, @password, @email);";
        await _dbConnection.ExecuteScalarAsync(query, user);
        // string hashedPassword = _passwordHashingService.HashPassword(user);
        return Ok(user);
      // }
    }
  }
}

