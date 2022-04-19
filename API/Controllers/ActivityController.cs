using Application.Models.Activity;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ActivityController : BaseController
{
    private readonly IActivityService _service;

    public ActivityController(
        IActivityService service
        )
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var activity = _service.GetById(id);
        return Ok(activity);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var activities = _service.GetAll();
        return Ok(activities);
    }

    [HttpPost]
    public IActionResult Post(ActivityPostRequest model)
    {
        _service.Post(model);
        return Ok(new { message = "Activity created successfully" });
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, ActivityUpdateRequest model)
    {
        _service.Update(id, model);
        return Ok(new { message = "Activity updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return Ok(new { message = "Activity deleted succesfully" });
    }
}
