using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Core.Entities.ViewModel;

public class RegisterVm
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }

    public string? Name { get; set; }
}