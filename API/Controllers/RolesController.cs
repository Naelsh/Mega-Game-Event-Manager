using Application.Helpers;
using Application.Models.Role;
using Application.Services;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
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
    [Authorize]
    public async Task<IActionResult> Put(int id, RoleUpdateRequest model)
    {
        try
        {
            await _service.Update(id, model);
        }
        catch (KeyNotFoundException knfe)
        {
            return NotFound(knfe.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(new { message = "Role updated successfully" });
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.Delete(id);
        }
        catch (KeyNotFoundException knfe)
        {
            return NotFound(knfe.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(new { message = "Role deleted succesfully" });
    }
}
