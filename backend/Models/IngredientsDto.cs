namespace backend.Models;

public record class IngredientsDto(
  string Name,
  int quantity,
  string QuantityType
);
