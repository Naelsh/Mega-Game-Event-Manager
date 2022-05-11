using Application.Helpers;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services;

public class BaseService
{
    internal readonly DataContext _context;

    public BaseService(DataContext context)
    {
        _context = context;
    }

    internal async Task<Role> GetRoleById(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null || role.IsDeleted)
            throw new KeyNotFoundException("Role not found");
        return role;
    }

    internal User GetUserById(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null || user.IsDeleted)
            throw new KeyNotFoundException("User not found");
        return user;
    }

    internal async Task<User> GetUserByUserName(string userName)
    {
        var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(x => x.Username == userName);
        if (user == null || user.IsDeleted)
            throw new KeyNotFoundException("User could not be found");
        return user;
    }

    internal async Task<Faction> GetFactionById(int id)
    {
        var faction = await _context.Factions.FindAsync(id);
        if (faction == null || faction.IsDeleted)
            throw new KeyNotFoundException("Faction not found");
        return faction;
    }

    internal async Task<Activity> GetActivityById(int id)
    {
        var activity = await _context.Activities.FindAsync(id);
        if (activity == null || activity.IsDeleted)
            throw new KeyNotFoundException("Event not found");
        return activity;
    }
}
