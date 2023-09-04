﻿using System.ComponentModel.DataAnnotations;

namespace Remind.Core.Models;

public class LoginModel
{
    [Required(ErrorMessage = "Email is required"), EmailAddress]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }
}