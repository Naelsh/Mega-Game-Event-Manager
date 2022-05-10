namespace Application.Services;

using Application.Helpers;
using Application.Models.Activity;
using Application.Models.Faction;
using Application.Models.Role;
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
    Task<IEnumerable<Faction>> GetFactionsForActivity(int id);
    Task<IEnumerable<Role>> GetRolesForActivity(int id);
    Task<DetailedActivity> GetDetailedById(int id);
    Task<Activity> GetById(int id);
    void AddUserToActivity(int id, AddUserToActivityRequest userName);
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
        var result = await _context.Activities.Where(ac => ac.IsDeleted != true).ToListAsync();
        if (result == null || result.Count == 0)
            throw new AppException("No activities found");
        return result;
    }

    public async Task<Activity> GetById(int id)
    {
        Activity activity = await GetActivityById(id);
        return activity;
    }

    public async Task<DetailedActivity> GetDetailedById(int id)
    {
        DetailedActivity? detailedActivity = await (from activity in _context.Activities
                                where activity.Id == id
                              select new DetailedActivity()
                              {
                                  Name = activity.Name,
                                  Description = activity.Description,
                                  StartDate = activity.StartDate,
                                  EndDate = activity.EndDate,
                                  Location = activity.Location,
                                  Factions = (from faction in activity.Factions
                                              select new DetailedFaction()
                                              {
                                                  Id = faction.Id,
                                                  Name = faction.Name,
                                                  Description = faction.Description,
                                                  ActivityId = id,
                                                  Roles = (from role in faction.Roles
                                                           select new DetailedRole()
                                                           {
                                                               Id = role.Id,
                                                               Name = role.Name,
                                                               Description = role.Description,
                                                               FactionId = faction.Id,
                                                               Users = role.Users
                                                           }).ToList() 
                                              }).ToList()
                                }).FirstOrDefaultAsync();
        if (detailedActivity == null)
            throw new NullReferenceException("Detailed activity not found");
        return detailedActivity;
    }

    public void AddUserToActivity(int id, AddUserToActivityRequest model)
    {
        var user = _context.Users.Include(u => u.Activities).FirstOrDefault(x => x.Username == model.UserName);
        if (user == null)
            throw new AppException("User not found");
        var activity = _context.Activities.Find(id);
        if (activity == null)
            throw new AppException("Activity not found");

        if (user.Activities.Contains(activity))
            throw new AppException("User already added to activity");

        user.Activities.Add(activity);
        _context.SaveChangesAsync();
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

