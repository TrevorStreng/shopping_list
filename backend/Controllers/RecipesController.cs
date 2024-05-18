using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
  [ApiController]
  [Route("[controller]")]

  public class RecipesController : ControllerBase
  {
    private readonly IDbConnection _dbConnection;

    public RecipesController(IDbConnection dbConnection) {
      _dbConnection = dbConnection;

    }

    [HttpGet]
    public async Task<IActionResult> GetAllRecipes() {
      try {
        string queryIgredients = "SELECT r.Id, r.name as RecipeName, i.name as IngredientName, ri.Quantity, ri.QuantityType FROM RecipeIngredients ri INNER JOIN Ingredients i on ri.IngredientId = i.Id INNER JOIN Recipes r on ri.IngredientId = i.Id ORDER by r.Id";
        var recipeIngredients = await _dbConnection.QueryAsync(queryIgredients);

        string querySteps = "SELECT r.Id, r.name as RecipeName, s.Description as steps from Recipes r Inner JOIN Steps s on r.Id = s.RecipeId";
        var recipeSteps = await _dbConnection.QueryAsync(querySteps);
        
        return Ok(new {recipeIngredients, recipeSteps});
      } catch(Exception ex) {
        return BadRequest(ex.Message);
      }
    }
  }

}

