using System.ComponentModel.DataAnnotations;

namespace Application.Models.Activity;

public class AddUserToActivityRequest
{
    [Required]
    [EmailAddress]
    public string UserName { get; set; } = "";
}
