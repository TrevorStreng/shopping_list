namespace backend.Models;

public record class RecipeDto(
  string name,
  List<IngredientsDto> recipes,
  List<StepsDto> steps
);
