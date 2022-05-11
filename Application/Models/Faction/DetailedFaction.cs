using Application.Models.Role;

namespace Application.Models.Faction;

public class DetailedFaction
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int ActivityId { get; set; }
    public List<DetailedRole> Roles { get; set; } = new List<DetailedRole>();
}
