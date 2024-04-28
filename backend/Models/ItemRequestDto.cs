using System.Security.Cryptography.X509Certificates;

namespace backend;

public record class ItemRequestDto(
  int id,
  string name
);
