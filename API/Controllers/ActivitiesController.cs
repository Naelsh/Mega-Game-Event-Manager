using Application.Authentication;
using Application.Helpers;
using Application.Models.Activity;
using Application.Services;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ActivitiesController : BaseController
{
    private readonly IActivityService _service;
    private readonly IMapper _mapper;

    public ActivitiesController(
        IActivityService service,
        IMapper mapper
        )
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<Activity> activities;
        try
        {
            activities = await _service.GetAll();
        }
        catch (AppException ae)
        {
            return NotFound(ae.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok(activities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Activity activity;
        try
        {
            activity = await _service.GetById(id);
        }
        catch (AppException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok(activity);
    }


    [HttpGet("{id}/details")]
    public async Task<IActionResult> GetDetailed(int id)
    {
        var detailedActivity = await _service.GetDetailedById(id);
        return Ok(detailedActivity);
    }

    [HttpGet("{id}/factions")]
    public async Task<IActionResult> GetFactions(int id)
    {
        var factions = await _service.GetFactionsForActivity(id);
        return Ok(factions);
    }

    [HttpGet("{id}/roles")]
    public async Task<IActionResult> GetRoles(int id)
    {
        var roles = await _service.GetRolesForActivity(id);
        return Ok(roles);
    }

    [HttpPost]
    public IActionResult Post(ActivityPostRequest model)
    {
        _service.Post(model);
        return Ok(new { message = "Activity created successfully" });
    }

    [HttpPost("{id}/add-user")]
    public IActionResult AddUser(int id, AddUserToActivityRequest model)
    {
        _service.AddUserToActivity(id, model);
        return Ok(new { messsage = "User added successfully" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ActivityUpdateRequest model)
    {
        await _service.Update(id, model);
        return Ok(new { message = "Activity updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return Ok(new { message = "Activity deleted succesfully" });
    }
}
