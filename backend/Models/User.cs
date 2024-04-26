using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace backend.Models;

public class User : IdentityUser
{
    [Required]
    public string? Password { get; set;}
    private List<Item>? Items {get; set;}

}
