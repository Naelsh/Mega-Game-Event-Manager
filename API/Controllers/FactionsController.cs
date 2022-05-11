using Application.Models.Faction;
using Application.Services;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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
        await _service.Post(model);
        return Ok(new { message = "Faction created successfully" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, FactionUpdateRequest model)
    {
        await _service.Update(id, model);
        return Ok(new { message = "Faction updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return Ok(new { message = "Faction deleted succesfully" });
    }
}
