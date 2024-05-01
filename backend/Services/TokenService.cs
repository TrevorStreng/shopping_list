using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services{
  public class TokenService
  {
    private IConfiguration _config;
    public TokenService(IConfiguration config) {
      _config = config;
    }

    public string GenerateJWT(int userId) {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
      var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sid, userId.ToString())
      };
      var token = new JwtSecurityToken(_config["Jwt:Issuer"],
        _config["Jwt:Issuer"],
        claims,
        expires: DateTime.Now.AddHours(72),
        signingCredentials: credentials);
      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }

}

