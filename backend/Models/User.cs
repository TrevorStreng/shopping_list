using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class User
{
    public int Id { get; set; }

    [Required]
    public string? UserName { get; set; }

    [Required]
    private string? Password { get; set; }

    [Required]
    private string? Email { get; set; }

}
