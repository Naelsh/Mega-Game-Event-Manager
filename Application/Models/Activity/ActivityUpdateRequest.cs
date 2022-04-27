using System.ComponentModel.DataAnnotations;

namespace Application.Models.Activity;

public class ActivityUpdateRequest
{
    [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1} characters", MinimumLength = 4)]
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [StringLength(10000, ErrorMessage = "{0} length must be less than {1} characters")]
    public string? Description { get; set; }
    [StringLength(100, ErrorMessage = "{0} length must be less than {1} characters")]
    public string? Location { get; set; }
}
