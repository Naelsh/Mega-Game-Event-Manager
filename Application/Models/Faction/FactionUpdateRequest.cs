namespace Application.Models.Faction;

public class FactionUpdateRequest
{
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int ActivityId { get; set; }
}
