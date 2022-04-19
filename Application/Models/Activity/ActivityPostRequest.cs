using System.ComponentModel.DataAnnotations;

namespace Application.Models.Activity;

public class ActivityPostRequest
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
}
