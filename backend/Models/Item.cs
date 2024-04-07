namespace backend.Models;

public class Item
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public int Amount { get; set; }

    public int? CategoryId { get; set; }

    // public Category? Category { get; set; }
}
