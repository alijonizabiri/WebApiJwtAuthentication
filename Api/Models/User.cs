using Microsoft.AspNetCore.Identity;

namespace WebApi.Models;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    public string? Image { get; set; }
}