﻿using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Models;
using backend.Services;
using Dapper;
using Microsoft.AspNetCore.Authorization;
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
    private readonly TokenService _tokenService;

    public UsersController(IDbConnection dbConnection, PasswordHashingService passwordHashingService, IConfiguration config, TokenService tokenService) {
      _dbConnection = dbConnection;
      _passwordHashingService = passwordHashingService;
      _config = config;
      _tokenService = tokenService;
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

    [HttpPost("CreateUser")]
    public async Task<ActionResult> CreateUser([FromBody]NewUserDto req) {
      System.Console.WriteLine(req);
      try {
        // check if email and password are in use
        if(await CheckUsername(req.Username)) return BadRequest(req.Username + "Username is already in use!");
        if(await CheckEmail(req.Email)) return BadRequest(req.Email + "Email is already in use!");

        // hash password
        string hashedPassword = _passwordHashingService.HashPassword(req.Password);
        System.Console.WriteLine(hashedPassword);

        string query = "INSERT INTO Users (username, password, email) values (@UserName, @PasswordHash, @Email);";
        var newUser = new User {UserName = req.Username, PasswordHash = hashedPassword, Email = req.Email};
        await _dbConnection.ExecuteAsync(query, newUser);

        // TODO: Need to drop jwt

        return Ok(newUser);
      } catch(Exception ex) {
        return BadRequest(ex.Message);
      }
    }

    // private string GenerateJWT(int userId) {
    //   var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
    //   var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    //   var claims = new[] {
    //     new Claim(JwtRegisteredClaimNames.Sid, userId.ToString())
    //   };

    //   var token = new JwtSecurityToken(_config["Jwt:Issuer"],
    //     _config["Jwt:Issuer"],
    //     claims,
    //     expires: DateTime.Now.AddHours(72),
    //     signingCredentials: credentials);
    //   return new JwtSecurityTokenHandler().WriteToken(token);
    // }

    private void setJwtHeader(string token) {
      Response.Cookies.Append("jwt", "Bearer " + token, new CookieOptions 
      {
        HttpOnly = true,
        Expires = DateTime.UtcNow.AddDays(30),
        SameSite = SameSiteMode.Lax,
        Secure = true
      });
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody]UserRequestDto req) {
      try {
        string query = "SELECT * FROM Users WHERE Username = @Username;";
        var user = await _dbConnection.QuerySingleOrDefaultAsync(query, new {Username = req.username});
        if(user == null) return BadRequest("Invalid username.🤬");
        

        bool passwordCheck = _passwordHashingService.VerifyPassword(user.Password, req.password);

        if(!passwordCheck) return BadRequest("Invalid Password..");

        var token = _tokenService.GenerateJWT(user.Id);

        if(token == null) return BadRequest("Problem generating token");

        setJwtHeader(token);

        return Ok(token);
      } catch(Exception ex) {
        return BadRequest(ex.Message);
      }
    }

    // public async Task<ActionResult> Logout() {
    //   set token to expired
    // }

    private int GetUserIdFromtoken(string jwtToken) {
    // Retrieve the Authorization header from the request
    // Request.Cookies.TryGetValue("jwt", out var jwtToken);

    // Check if the Authorization header exists and starts with "Bearer "
    if (jwtToken != null && jwtToken.StartsWith("Bearer "))
    {
        // Extract the JWT token from the Authorization header
        string token = jwtToken.Substring("Bearer ".Length).Trim();

        // Now you can parse and validate the JWT token to extract user information, such as username
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)),
            ValidateIssuer = true,
            ValidIssuer = _config["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = _config["Jwt:Issuer"],
            ValidateLifetime = true
        }, out securityToken);
        
        // Retrieve the username claim from the validated claims principal
        Claim usernameClaim = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Sid)!;
        if (usernameClaim != null)
        {
            // Return the id extracted from the token
            return int.Parse(usernameClaim.Value);
        }
    }
    return -1; 
    }

    // [Authorize]
    [HttpGet("GetUserItems")]
    public async Task<ActionResult> GetUsersItems() {

      //TODO: this is constantly getting called even after updating quantity
      //! get user from token
      try {
        int userId = 0;
        Request.Cookies.TryGetValue("jwt", out var jwtToken);

        jwtToken ??= Request.Headers.Authorization;
        if(jwtToken != null) {
          userId = GetUserIdFromtoken(jwtToken);
        }

        string query = "select UserItems.UserId, Items.name as item, UserItems.itemQuantity, Categories.name as category from UserItems inner join Items on UserItems.ItemId = items.id inner join categories on items.categoryId = categories.id where userId = @userId;";
        var items = await _dbConnection.QueryAsync(query, new {UserId = userId});

        return Ok(items);
      } catch(Exception ex) {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("AddUserItem")]
    public async Task<ActionResult> AddUserItem([FromBody]UserItemDto req) {
      try {
        // check for category
        string findCategoryQuery = "select * from categories where name = @Name;";
        var category = await _dbConnection.QuerySingleOrDefaultAsync(findCategoryQuery, new {Name = req.itemCategory});
        if(category == null) {
          string insertCategoryQuery = "insert into categories (name) values (@Name);";
          await _dbConnection.ExecuteAsync(insertCategoryQuery, new {Name = req.itemCategory});
          category = await _dbConnection.QuerySingleOrDefaultAsync(findCategoryQuery, new {Name = req.itemCategory});
        }

        if(category == null) return BadRequest("Error adding or finding category..");

        // check if item exists and create it if not
        string findItemQuery = "select * from items where name = @Name";
        var item = await _dbConnection.QuerySingleOrDefaultAsync(findItemQuery, new {Name = req.itemName});
        if(item == null || category == null) {
          string insertItemQuery = "insert into items(name, categoryId) values (@Name, @categoryId);";
          await _dbConnection.ExecuteAsync(insertItemQuery, new {Name = req.itemName, categoryId = category!.Id});
          item = await _dbConnection.QuerySingleOrDefaultAsync(findItemQuery, new {Name = req.itemName});
        }

        if(item == null) return BadRequest("Error adding or finding item..");

        // find user
        int userId = 0;
        Request.Cookies.TryGetValue("jwt", out var jwtToken);

        jwtToken ??= Request.Headers.Authorization;
        if(jwtToken != null) {
          userId = GetUserIdFromtoken(jwtToken);
        }
        // userId = GetUserIdFromtoken(jwtToken);

        string findUserQuery = "select * from users where id = @Id";
        var user = await _dbConnection.QuerySingleOrDefaultAsync(findUserQuery, new {Id = userId});

        if(user == null) return BadRequest("User not found..");

        // add item userItems
        string AddItemToUserQuery = "insert into UserItems (userId, itemId, itemQuantity) values (@userId, @itemId, @itemQuantity);";
        await _dbConnection.ExecuteAsync(AddItemToUserQuery, new {userId = user.Id, itemId = item.Id, req.itemQuantity});

        return Ok(new {item, category});
      } catch(Exception ex) {
        return BadRequest(ex.Message);
      }
    }

    [HttpDelete("RemoveUserItem")]
    public async Task<IActionResult> RemoveUserItem([FromQuery]string itemName) {
      try {
        // int userId = GetUserIdFromtoken();
        int userId = 0;
        Request.Cookies.TryGetValue("jwt", out var jwtToken);

        jwtToken ??= Request.Headers.Authorization;

        if(jwtToken != null) {
          userId = GetUserIdFromtoken(jwtToken);
        }

        string query = "DELETE ui FROM UserItems AS ui JOIN Items AS i on ui.itemId = i.id WHERE ui.userId = @UserId AND i.name = @ItemName;";
        await _dbConnection.ExecuteAsync(query, new { userId, itemName});

        return Ok("Item removed from User");
      } catch(Exception ex) {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("UpdateUserItemQuantity")]
    public async Task<IActionResult> UpdateUserItemQuantity([FromBody] UserItemDto req) {
      try {
        // int userId = GetUserIdFromtoken();

        int userId = -1;
        Request.Cookies.TryGetValue("jwt", out var jwtToken);

        jwtToken ??= Request.Headers.Authorization;

        if(jwtToken != null) {
          userId = GetUserIdFromtoken(jwtToken);
        }

        if(userId == -1) {
          return BadRequest("No user found..");
        }
        System.Console.WriteLine(req);


        string query = "UPDATE ui SET ui.itemQuantity = @Quantity FROM UserItems ui INNER JOIN Items i on ui.ItemId = i.id INNER JOIN Users u ON ui.userId = u.id WHERE u.id = @userId AND i.name = @ItemName;";
        await _dbConnection.ExecuteAsync(query, new {Quantity = req.itemQuantity, userId, ItemName = req.itemName });

        return NoContent();
      } catch(Exception ex) {
        return BadRequest(ex.Message);
      }
    }

  }
}

// Out of context twitch clips

