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
    Task<Role> GetById(int id);
    Task AddUserToRole(int id, AddUserToRoleRequest model);
    Task Post(RolePostRequest model);
    Task Delete(int id);
    Task Update(int id, RoleUpdateRequest model);
}

public class RoleService : BaseService, IRoleService
{
    private readonly IMapper _mapper;

    public RoleService(DataContext context, IMapper mapper) : base(context)
    {
        _mapper = mapper;
    }

    public async Task<Role> GetById(int id)
    {
        var role = await GetRoleById(id);
        return role;
    }

    public async Task Post(RolePostRequest model)
    {
        var role = _mapper.Map<Role>(model);
        var faction = await GetFactionById(model.FactionId);

        role.Faction = faction;

        await _context.Roles.AddAsync(role);
        _context.SaveChanges();
    }

    public async Task AddUserToRole(int id, AddUserToRoleRequest model)
    {
        var user = await GetUserByUserName(model.Username);
        var role = await GetRoleById(id);

        if (user.Roles.Contains(role))
            throw new AppException("Role allready added to user");

        user.Roles.Add(role);
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

    public async Task Delete(int id)
    {
        var role = await GetRoleById(id);

        role.IsDeleted = true;

        _context.Roles.Update(role);
        _context.SaveChanges();
    }

}