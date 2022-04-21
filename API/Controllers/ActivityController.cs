﻿using Application.Helpers;
using Application.Models.Activity;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers;

public class ActivityController : BaseController
{
    private readonly IActivityService _service;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;

    public ActivityController(
        IActivityService service,
        IMapper mapper,
        IOptions<AppSettings> appSettings
        )
    {
        _service = service;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var activity = await _service.GetById(id);
        return Ok(activity);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var activities = await _service.GetAll();
        return Ok(activities);
    }

    [HttpPost]
    public IActionResult Post(ActivityPostRequest model)
    {
        _service.Post(model);
        return Ok(new { message = "Activity created successfully" });
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
