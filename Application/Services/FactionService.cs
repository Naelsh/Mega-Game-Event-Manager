namespace Application.Services;

using Application.Helpers;
using Application.Models.Faction;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFactionService
{
    Task<IEnumerable<Faction>> GetAllFactionForEventByID(int activityId);
    Task<Faction> GetById(int id);
    Task Post(FactionPostRequest model);
    Task Delete(int id);
    Task Update(int id, FactionUpdateRequest model);
}

public class FactionService : IFactionService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public FactionService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Faction>> GetAllFactionForEventByID(int activityId)
    {
        return await _context.Factions.Where(
            f => !f.IsDeleted &&
            f.Activity.Id == activityId
            ).ToListAsync();
    }

    public async Task<Faction> GetById(int id)
    {
        Faction faction = await GetFactionById(id);
        return faction;
    }

    public async Task Post(FactionPostRequest model)
    {
        var faction = _mapper.Map<Faction>(model);
        Activity activity = await GetActivityById(model.ActivityId);

        faction.Activity = activity;

        _context.Factions.Add(faction);
        _context.SaveChanges();
    }

    public async Task Delete(int id)
    {
        var faction = await GetFactionById(id);

        faction.IsDeleted = true;

        _context.Factions.Update(faction);
        _context.SaveChanges();
    }

    public async Task Update(int id, FactionUpdateRequest model)
    {
        var faction = await GetFactionById(id);
        Activity activity = await GetActivityById(model.ActivityId);

        _mapper.Map(model, faction);
        faction.Activity = activity;

        _context.Factions.Update(faction);
        _context.SaveChanges();
    }

    private async Task<Activity> GetActivityById(int activityId)
    {
        var activity = await _context.Activities.FindAsync(activityId);
        if (activity == null)
            throw new AppException("Activity could not be found");
        if (activity.IsDeleted)
            throw new AppException("Activity could not be found");
        return activity;
    }

    private async Task<Faction> GetFactionById(int id)
    {
        var faction = await _context.Factions.FindAsync(id);
        if (faction == null)
            throw new KeyNotFoundException("Faction not found");
        if (faction.IsDeleted)
            throw new AppException("Faction not found");
        return faction;
    }
}

