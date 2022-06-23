namespace WebApi.Models;

public class UserViewModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int Age { get; set; }
    public IFormFile? Image { get; set; }
}