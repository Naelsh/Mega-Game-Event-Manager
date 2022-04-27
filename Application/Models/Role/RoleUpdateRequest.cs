using System.ComponentModel.DataAnnotations;

namespace Application.Models.Role;

public class RoleUpdateRequest
{
    [StringLength(50, MinimumLength = 1, ErrorMessage = "{0} length must be between {2} and {1} characters")]
    public string Name { get; set; } = "";
    [StringLength(10000, MinimumLength = 1, ErrorMessage = "{0} length must be between {2} and {1} characters")]
    public string Description { get; set; } = "";
    [Range(1, int.MaxValue, ErrorMessage = "{0} needs to be a positive integer")]
    public int FactionId { get; set; } = 0;
    [Range(1, int.MaxValue, ErrorMessage = "{0} needs to be a positive integer")]
    public int UserId { get; set; }
}
