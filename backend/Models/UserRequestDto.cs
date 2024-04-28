namespace backend;

public record class UserRequestDto(
  int id,
  string username,
  string password
);
