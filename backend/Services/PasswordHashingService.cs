using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Services
{
  public class PasswordHashingService
  {
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordHashingService(IPasswordHasher<User> passwordHasher) {
      _passwordHasher = passwordHasher;
    }

    public string HashPassword(string password) {
      #nullable disable
      return _passwordHasher.HashPassword(null, password);
      #nullable restore
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword) {
      #nullable disable
      var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
      #nullable restore
      return result == PasswordVerificationResult.Success;
    }
  }
}

