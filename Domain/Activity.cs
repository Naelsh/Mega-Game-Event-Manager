using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class Activity
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; } = "";
    public string Location { get; set; } = "";
    public bool IsDeleted { get; set; } = false;

    public List<Faction> Factions { get; set; } = new List<Faction>();
    public List<User> Participants { get; set; } = new List<User>();
}
