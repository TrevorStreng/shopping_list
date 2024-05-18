using System.Security.Cryptography.X509Certificates;

namespace backend.Models;

public record class ItemRequestDto(
  int id,
  string name
);
