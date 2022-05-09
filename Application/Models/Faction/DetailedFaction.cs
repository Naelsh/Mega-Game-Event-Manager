using Application.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Faction;

public class DetailedFaction
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int ActivityId { get; set; }
    public List<DetailedRole> Roles { get; set; } = new List<DetailedRole>();
}
