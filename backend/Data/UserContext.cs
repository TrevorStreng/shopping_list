using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class UserContext(DbContextOptions<UserContext> options)
 : DbContext(options)
{
  public DbSet<User> Users { get; set; }
}
