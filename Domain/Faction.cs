using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class Faction
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public bool IsDeleted { get; set; } = false;

    public List<Role> Roles { get; set; } = new List<Role>();
    public Activity? Activity { get; set; }
}
