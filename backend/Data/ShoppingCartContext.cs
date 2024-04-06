using Microsoft.EntityFrameworkCore;
using shoppingList.Api.Models;


namespace shoppingList.Api.Data;

public class ShoppingCartContext(DbContextOptions<ShoppingCartContext> options)
 : DbContext(options)
{
  public DbSet<Item> Items { get; set; }
}
