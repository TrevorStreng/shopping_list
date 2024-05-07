using backend.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Dapper;
using System.Data;
using System.Data.Common;

namespace backend.Controllers 
{
  [ApiController]
  [Route("[controller]")]
  public class ItemsController : ControllerBase
  {
    private readonly IDbConnection _dbConnection;

    public ItemsController(IDbConnection dbConnection) {
      _dbConnection = dbConnection;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllItems() {
      // var items = await _dbConnection.QueryAsync<Item>("SELECT * FROM Items");

      return Ok("here");
    }

    [HttpGet("{id}", Name = "GetItem")]
    public async Task<ActionResult> GetItem(int id) {
      var item = await _dbConnection.QueryAsync<Item>("SELECT * FROM Items WHERE Id = @id", new { Id = id});
      if(item == null) return NotFound();

      return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult> CreateItem(Item newItem) {

      try {
        string query = "INSERT INTO Items (name, amount, categoryId) values (@name, @amount, @categoryId); SELECT LAST_INSERT_ID();";
        var itemId = await _dbConnection.ExecuteScalarAsync<ulong>(query, newItem);
        System.Console.WriteLine("Inserted id:", itemId);
        // !! this is not returning the id but otherwise works
        return CreatedAtRoute("GetItem", new { id = itemId}, newItem);
      } catch (Exception ex) {
        System.Console.WriteLine(ex);
        return StatusCode(500, "An error occured while creating item. 🤬");
      }

    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItem(int id, Item updatedItem) {
      // ! this method only allows updating whole object not partial / make patch request if needed
      var item = await _dbConnection.QuerySingleOrDefaultAsync<Item>("SELECT * FROM Items WHERE Id = @Id;", new{Id = id});
      System.Console.WriteLine(item);

      if(item == null) return NotFound();

      var newItem = new {
        updatedItem.Name,
        updatedItem.Amount,
        updatedItem.CategoryId,
        Id = id
      };

      
      try {
        string query = "UPDATE Items SET Name = @Name, Amount = @Amount, CategoryId = @CategoryId WHERE Id = @Id;";
        await _dbConnection.ExecuteAsync(query, newItem);
      } catch(Exception ex) {
        System.Console.WriteLine($"{ex}");
        return StatusCode(500, "An error occured while updating item. 🤬");
      }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItem(int id) {    
      var item = await _dbConnection.QueryAsync("SELECT * FROM Items WHERE Id = @Id", new{Id = id});

      if(item == null) return NotFound();

      try {
        await _dbConnection.ExecuteAsync("DELETE FROM Items WHERE Id = @Id", new{Id = id});
      } catch(Exception ex) {
        System.Console.WriteLine(ex);
        return StatusCode(500, "An error occured while deleting item. 🤬");
      }
      
      return NoContent();
    }

  }
}

