using System.ComponentModel.DataAnnotations;

namespace Application.Models.Role;

public class RolePostRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = "";
    [Required]
    public string Description { get; set; } = "";
    [Required]
    public int FactionId { get; set; } = 0;
}
