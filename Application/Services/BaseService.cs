using Domain;
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
}
