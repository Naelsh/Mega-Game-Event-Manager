using Application.Models.Activity;
using Application.Models.Faction;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers;

public class FactionController : BaseController
{
    private readonly IFactionService _service;
    private readonly IMapper _mapper;

    public FactionController(
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
        var faction = await _service.GetById(id);
        return Ok(faction);
    }

    [HttpGet("{activityId}")]
    public async Task<IActionResult> GetAllForActivity(int activityId)
    {
        var factions = await _service.GetAllFactionForEventByID(activityId);
        return Ok(factions);
    }

    [HttpPost]
    public IActionResult Post(FactionPostRequest model)
    {
        _service.Post(model);
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
