namespace backend.Models;

public record class UserItemDto(
  string itemName,
  int itemQuantity,
  string? itemCategory
);
