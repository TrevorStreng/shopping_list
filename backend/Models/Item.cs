using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Item
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public float Amount { get; set; }

    [Required]
    public int CategoryId { get; set; }

    // public Category? Category { get; set; }
}
