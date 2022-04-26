namespace Application.Services;

using Application.Helpers;
using Application.Models.Activity;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IActivityService
{
    Task<IEnumerable<Activity>> GetAll();
    Task<Activity> GetById(int id);
    void Post(ActivityPostRequest model);
    Task Delete(int id);
    Task Update(int id, ActivityUpdateRequest model);
}

public class ActivityService : IActivityService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ActivityService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Activity>> GetAll()
    {
        return await _context.Activities.Where(ac => ac.IsDeleted != true).ToListAsync();
    }

    public async Task<Activity> GetById(int id)
    {
        Activity activity = await GetActivityById(id);
        return activity;
    }

    public void Post(ActivityPostRequest model)
    {
        // validation
        if (DateTime.Compare(model.StartDate, model.EndDate) > 0)
            throw new AppException("Event '" + model.Name + "' start date after end date");

        var activity = _mapper.Map<Activity>(model);

        _context.Activities.Add(activity);
        _context.SaveChanges();
    }

    public async Task Delete(int id)
    {
        var activity = await GetActivityById(id);
        activity.IsDeleted = true;
        _context.Activities.Update(activity);
        _context.SaveChanges();
    }

    public async Task Update(int id, ActivityUpdateRequest model)
    {
        var activity = await GetActivityById(id);

        if (DateTime.Compare(model.StartDate, model.EndDate) > 0)
            throw new AppException("Event '" + model.Name + "' start date after end date");

        _mapper.Map(model, activity);
        _context.Activities.Update(activity);
        _context.SaveChanges();
    }

    private async Task<Activity> GetActivityById(int id)
    {
        var activity = await _context.Activities.FindAsync(id);
        if (activity == null)
            throw new KeyNotFoundException("Event not found");
        if (activity.IsDeleted)
            throw new AppException("Event not found");
        return activity;
    }
}

