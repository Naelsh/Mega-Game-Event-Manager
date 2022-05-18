using Application.Models.Faction;
using Application.Services;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class FactionsController : BaseController
{
    private readonly IFactionService _service;
    private readonly IMapper _mapper;

    public FactionsController(
        IFactionService service,
        IMapper mapper
        )
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Faction faction;
        try
        {
            faction = await _service.GetById(id);
        }
        catch (KeyNotFoundException knfe)
        {
            return NotFound(knfe.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(faction);
    }

    [HttpPost]
    public async Task<IActionResult> Post(FactionPostRequest model)
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
        return Ok(new { message = "Faction created successfully" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, FactionUpdateRequest model)
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
        return Ok(new { message = "Faction updated successfully" });
    }

    [HttpDelete("{id}")]
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
        return Ok(new { message = "Faction deleted succesfully" });
    }
}
