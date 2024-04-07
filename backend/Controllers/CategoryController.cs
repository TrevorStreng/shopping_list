using Microsoft.AspNetCore.Mvc;
using backend.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CategoryController : ControllerBase
  {
    private readonly ShoppingCartContext _context;
    public CategoryController(ShoppingCartContext context) {
      _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories() {
      var categories = await _context.Categories.ToListAsync();
      return Ok(categories);
    }
  }

}

