namespace Application.Services;

using Application.Helpers;
using Application.Models.Role;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRoleService
{
    Task<IEnumerable<Role>> GetAllRolesForFactionByID(int factionId);
    Task<Role> GetById(int id);
    void Post(RolePostRequest model);
    Task Delete(int id);
    Task Update(int id, RoleUpdateRequest model);
}

public class RoleService : IRoleService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public RoleService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Role>> GetAllRolesForFactionByID(int factionId)
    {
        return await _context.Roles.Where(
            r => !r.IsDeleted &&
            r.Faction.Id == factionId
            ).ToListAsync();
    }

    public async Task<Role> GetById(int id)
    {
        var role = await GetRoleById(id);
        return role;
    }

    public async void Post(RolePostRequest model)
    {
        var role = _mapper.Map<Role>(model);
        var faction = await GetFactionById(model.FactionId);

        role.Faction = faction;

        _context.Roles.Add(role);
        _context.SaveChanges();
    }

    public async Task Delete(int id)
    {
        var role = await GetRoleById(id);

        role.IsDeleted = true;

        _context.Roles.Update(role);
        _context.SaveChanges();
    }

    public async Task Update(int id, RoleUpdateRequest model)
    {
        var role = await GetRoleById(id);
        var faction = await GetFactionById(model.FactionId);

        _mapper.Map(model, role);
        role.Faction = faction;

        _context.Roles.Update(role);
        _context.SaveChanges();
    }

    private async Task<Faction> GetFactionById(int id)
    {
        var faction = await _context.Factions.FindAsync(id);
        if (faction == null)
            throw new AppException("Activity could not be found");
        if (faction.IsDeleted)
            throw new AppException("Activity could not be found");
        return faction;
    }

    private async Task<Role> GetRoleById(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null)
            throw new KeyNotFoundException("Role not found");
        if (role.IsDeleted)
            throw new AppException("Role not found");
        return role;
    }
}