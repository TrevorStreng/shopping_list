namespace backend.Models;

public record class NewUserDto(
  string Username,
  string Password,
  string Email
);