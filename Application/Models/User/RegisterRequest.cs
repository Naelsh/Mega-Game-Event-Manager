﻿using System.ComponentModel.DataAnnotations;

namespace Application.Models.User;

public class RegisterRequest
{
    [Required]
    public string FirstName { get; set; } = "";

    [Required]
    public string LastName { get; set; } = "";

    [EmailAddress]
    [Required]
    public string Username { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}
