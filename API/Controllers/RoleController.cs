using Application.Models.Role;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class RoleController : BaseController
{
    private readonly IRoleService _service;
    private readonly IMapper _mapper;

    public RoleController(
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
        var role = await _service.GetById(id);
        return Ok(role);
    }

    [HttpGet("getAll/{factionId}")]
    public async Task<IActionResult> GetAllForActivity(int factionID)
    {
        var factions = await _service.GetAllRolesForFactionByID(factionID);
        return Ok(factions);
    }

    [HttpPost]
    public IActionResult Post(RolePostRequest model)
    {
        _service.Post(model);
        return Ok(new { message = "Role created successfully" });
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
