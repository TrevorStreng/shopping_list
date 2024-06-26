﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;

namespace backend.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class CategoriesController : ControllerBase
  {
     private readonly IDbConnection _dbConnection;
    public CategoriesController(IDbConnection dbConnection) {
      _dbConnection = dbConnection;
    }

    // [HttpGet]
    // public async Task<IActionResult> GetAllCategories() {
    //   var categories = await _dbConnection.Categories.ToListAsync();
    //   return Ok(categories);
    // }

    // [HttpGet("{id}", Name = "GetCategory")]
    // public async Task<IActionResult> GetCategory(int id) {
    //   var category = await _context.Categories.FindAsync(id);
    //   if(category == null) {
    //     return NotFound();
    //   }
    //   return Ok(category);
    // }

    // [HttpPost]
    // public async Task<IActionResult> PostCategory(Category newCategory) {
    //   _context.Categories.Add(newCategory);

    //   try {
    //     await _context.SaveChangesAsync();
    //   } catch (Exception ex) {
    //     System.Console.WriteLine(ex);
    //     return StatusCode(500, "An error occured while creating item. 🤬");
    //   }

    //   return CreatedAtRoute("GetCategory", new { id = newCategory.Id }, newCategory);
    // }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> updateCategory(int id, Category updatedCategory) {
    //   var category = await _context.Categories.FindAsync(id);

    //   if(category == null) return NotFound();

    //   category.Name = updatedCategory.Name;

    //   try {
    //     await _context.SaveChangesAsync();
    //   } catch(Exception ex) {
    //     System.Console.WriteLine(ex);
    //     return StatusCode(500, "An error occured while updating item. 🤬");
    //   }
      
    //   return NoContent();
    // }
  }

}

