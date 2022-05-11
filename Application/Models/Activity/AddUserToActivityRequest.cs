using System.ComponentModel.DataAnnotations;

namespace Application.Models.Activity;

public class AddUserToActivityRequest
{
    [EmailAddress]
    public string UserName { get; set; } = "";
}
