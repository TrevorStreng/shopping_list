using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers 
{
  [ApiController]
  [Route("[controller]")]
  public class ItemsController : ControllerBase
  {
    private readonly ShoppingCartContext _context;

    public ItemsController(ShoppingCartContext context) {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllItems() {
      var items = await _context.Items.ToListAsync();
      return Ok(items);
    }

    [HttpGet("{id}", Name = "GetItem")]
    public async Task<ActionResult> GetItem(int id) {
      var item = await _context.Items.FindAsync(id);
      if(item == null) return NotFound();

      return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult> CreateItem(Item newItem) {
      _context.Items.Add(newItem);

      try {
        await _context.SaveChangesAsync();
      } catch (Exception ex) {
        System.Console.WriteLine(ex);
        return StatusCode(500, "An error occured while creating item. 🤬");
      }

      return CreatedAtRoute("GetItem", new { id = newItem.Id}, newItem);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItem(int id, Item updatedItem) {
      // ! this method only allows updating whole object not partial / make patch request if needed
      var item = await _context.Items.FindAsync(id);

      if(item == null) return NotFound();

      item.Name = updatedItem.Name;    
      item.Amount = updatedItem.Amount;    
      item.CategoryId = updatedItem.CategoryId;
      
      try {
        await _context.SaveChangesAsync();
      } catch(Exception ex) {
        System.Console.WriteLine($"{ex}");
        return StatusCode(500, "An error occured while updating item. 🤬");
      }
      
      return NoContent();
    }

  }
}

