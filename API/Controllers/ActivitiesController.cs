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
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (AppException ae)
        {
            return NotFound(ae.Message);
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
        DetailedActivity detailedActivity;
        try
        {
            detailedActivity = await _service.GetDetailedById(id);
        }
        catch (NullReferenceException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok(detailedActivity);
    }

    [HttpPost("{id}/add-user")]
    public async Task<IActionResult> AddUser(int id, AddUserToActivityRequest model)
    {
        try
        {
            await _service.AddUserToActivity(id, model);
        }
        catch (AppException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok(new { messsage = "User added successfully" });
    }

    [HttpPost]
    public IActionResult Post(ActivityPostRequest model)
    {
        try
        {
            _service.Post(model);
        }
        catch (AppException e)
        {
            return BadRequest(e.Message);
        }
        return Ok(new { message = "Activity created successfully" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ActivityUpdateRequest model)
    {
        try
        {
            await _service.Update(id, model);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (AppException ae)
        {
            return NotFound(ae.Message);
        }
        catch (ArgumentException ae)
        {
            return BadRequest(ae.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok(new { message = "Activity updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.Delete(id);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (AppException ae)
        {
            return NotFound(ae.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok(new { message = "Activity deleted succesfully" });
    }
}
