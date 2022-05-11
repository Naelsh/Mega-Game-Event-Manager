using Application.Helpers;
using Application.Models.Role;
using Application.Services;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class RolesController : BaseController
{
    private readonly IRoleService _service;
    private readonly IMapper _mapper;

    public RolesController(
        IRoleService service,
        IMapper mapper
        )
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Role role;
        try
        {
            role = await _service.GetById(id);
        }
        catch (KeyNotFoundException knfe)
        {
            return NotFound(knfe.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> Post(RolePostRequest model)
    {
        try
        {
            await _service.Post(model);
        }
        catch (KeyNotFoundException knfe)
        {
            return NotFound(knfe.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(new { message = "Role created successfully" });
    }

    [HttpPost("{id}/add-user")]
    public async Task<IActionResult> AddUserToRole(int id, AddUserToRoleRequest model)
    {
        try
        {
            await _service.AddUserToRole(id, model);
        }
        catch (KeyNotFoundException knfe)
        {
            return NotFound(knfe.Message);
        }
        catch (AppException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(new { message = "User added successfully" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, RoleUpdateRequest model)
    {
        await _service.Update(id, model);
        return Ok(new { message = "Role updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return Ok(new { message = "Role deleted succesfully" });
    }
}
