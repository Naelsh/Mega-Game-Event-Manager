using System.ComponentModel.DataAnnotations;

namespace Application.Models.Activity;

public class ActivityPostRequest
{
    [Required]
    [StringLength(60, MinimumLength = 1)]
    public string? Name { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Start Date")]
    [Range(typeof(DateTime),"1/1/2020","1/1/2999")]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name ="End Date")]
    [Range(typeof(DateTime),"1/1/2020","1/1/2999")]
    public DateTime EndDate { get; set; }

    public string? Description { get; set; }
    public string? Location { get; set; }
}
