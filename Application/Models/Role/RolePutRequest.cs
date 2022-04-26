namespace Application.Models.Role;

public class RolePutRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int FactionId { get; set; } = 0;
    public int UserId { get; set; }
}
