namespace Application.Models.Role;

public class DetailedRole
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int FactionId { get; set; } = 0;
    public List<Domain.User> Users { get; set; } = new List<Domain.User>();
}
