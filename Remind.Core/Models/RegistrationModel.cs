using System.ComponentModel.DataAnnotations;

namespace Remind.Core.Models;

public class RegistrationModel
{
    [Required(ErrorMessage = "Name is required")]
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}