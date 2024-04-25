using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace backend.Models;

public class User : IdentityUser
{
    // public int Id { get; set; }

    // [Required]
    // public string? UserName { get; set; }

    // [Required]
    // private string? Password { get; set; }

    // [Required]
    // private string? Email { get; set; }

    private List<Item>? Items {get; set;}

}
