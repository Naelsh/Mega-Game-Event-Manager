using System.ComponentModel.DataAnnotations;

namespace Application.Models.Faction;

public class FactionPostRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int ActivityId { get; set; }
}
