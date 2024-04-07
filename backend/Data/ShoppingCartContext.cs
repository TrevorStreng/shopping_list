using backend.Models;
using Microsoft.EntityFrameworkCore;


namespace backend.Data;

public class ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
 : DbContext(options)
{
  public DbSet<Item> Items { get; set; }

  public DbSet<Category> Categories { get; set; }
}
