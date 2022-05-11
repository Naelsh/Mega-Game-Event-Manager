using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

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

    internal async Task<User> GetUserById(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null || user.IsDeleted)
            throw new KeyNotFoundException("User not found");
        return user;
    }

    internal async Task<User> GetUserWithRolesByUserName(string userName)
    {
        var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(x => x.Username == userName);
        if (user == null || user.IsDeleted)
            throw new KeyNotFoundException("User could not be found");
        return user;
    }

    internal async Task<User> GetUserWithActivityByUserName(string userName)
    {
        var user = await _context.Users.Include(u => u.Activities).FirstOrDefaultAsync(x => x.Username == userName);
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
