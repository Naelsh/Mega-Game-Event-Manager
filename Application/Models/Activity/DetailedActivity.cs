using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Faction;
using Domain;

namespace Application.Models.Activity;

public class DetailedActivity
{
    public string? Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public List<DetailedFaction> Factions { get; set; } = new List<DetailedFaction>();
}
