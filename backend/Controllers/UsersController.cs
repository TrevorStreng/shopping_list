using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using backend.Models;
using backend.Services;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace backend.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UsersController : ControllerBase
  {
    private readonly IDbConnection _dbConnection;
    private readonly PasswordHashingService _passwordHashingService;
    private IConfiguration _config;

    public UsersController(IDbConnection dbConnection, PasswordHashingService passwordHashingService, IConfiguration config) {
      _dbConnection = dbConnection;
      _passwordHashingService = passwordHashingService;
      _config = config;
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

    public async Task<bool> CheckUsername(string username) {
        string usernameQuery = "SELECT * FROM Users WHERE username = @username";

        var existingUser = await _dbConnection.QueryFirstOrDefaultAsync<User>(usernameQuery, new {Username = username});

        if(existingUser != null) return true;
        return false;
    }
    public async Task<bool> CheckEmail(string email) {
        string emailQuery = "SELECT * FROM Users WHERE email = @email";

        var existingUser = await _dbConnection.QueryFirstOrDefaultAsync<User>(emailQuery, new {Email = email});

        if(existingUser != null) return true;
        return false;
    }

    [HttpPost]
    [Route("CreateUser")]
    public async Task<ActionResult> CreateUser([FromBody]User user) {
      try {
        // check if email and password are in use
        if(await CheckUsername(user.UserName!)) return BadRequest(user.UserName + "Username is already in use!");
        if(await CheckEmail(user.Email!)) return BadRequest(user.Email + "Email is already in use!");

        // hash password
        string hashedPassword = _passwordHashingService.HashPassword(user.PasswordHash!);

        string query = "INSERT INTO Users (username, password, email) values (@UserName, @PasswordHash, @Email);";
        var newUser = new User {UserName = user.UserName, PasswordHash = hashedPassword, Email = user.Email};
        await _dbConnection.ExecuteAsync(query, newUser);

        return Ok(newUser);
      } catch(Exception ex) {
        return BadRequest(ex.Message);
      }
    }

    private string GenerateJWT() {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        _config["Jwt:Issuer"],
        null,
        expires: DateTime.Now.AddHours(72),
        signingCredentials: credentials);
      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult> Login([FromBody]User req) {
      try {
        string query = "SELECT * FROM Users WHERE Username = @Username;";
        var user = await _dbConnection.QuerySingleOrDefaultAsync(query, new {Username = req.UserName});
        if(user == null) return BadRequest("Invalid username.🤬");
        
        bool passwordCheck = _passwordHashingService.VerifyPassword(user.password, req.Password!);

        req.Password = null;

        if(!passwordCheck) return BadRequest("Invalid Password..");

        // TODO: If password check passes create and send sign in token
        var token = GenerateJWT();

        Response.Headers.Append("Authorization", "Bearer" + token);

        return Ok(req.UserName + " logged in" + new {token});
      } catch(Exception ex) {
        return BadRequest(ex.Message);
      }
    }
  }
}

