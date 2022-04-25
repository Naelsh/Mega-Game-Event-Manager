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
    void Post(FactionPostRequest model);
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
        return await _context.Factions.Where(f => f.Activity.Id == activityId).ToListAsync();
    }

    public async Task<Faction> GetById(int id)
    {
        Faction? faction = await GetFactionById(id);
        if (faction == null)
            throw new AppException("Faction not found");
        return faction;
    }

    public void Post(FactionPostRequest model)
    {
        var faction = _mapper.Map<Faction>(model);
        var activity = _context.Activities.Find(model.ActivityId);
        if (activity == null)
            throw new AppException("Activity not found when creating faction", faction);

        faction.Activity = activity;

        _context.Factions.Add(faction);
        _context.SaveChanges();
    }

    public async Task Delete(int id)
    {
        var faction = await GetFactionById(id);
        if (faction == null)
            throw new AppException("Faction could not be found");

        _context.Factions.Remove(faction);
        _context.SaveChanges();
    }

    public async Task Update(int id, FactionUpdateRequest model)
    {
        var faction = await GetFactionById(id);
        if (faction == null)
            throw new AppException("Faction could not be found");
        var activity = await _context.Activities.FindAsync(model.ActivityId);
        if (activity == null)
            throw new AppException("Activity could not be found");

        _mapper.Map(model, faction);
        faction.Activity = activity;

        _context.Factions.Update(faction);
        _context.SaveChanges();
    }

    private async Task<Faction> GetFactionById(int id)
    {
        var faction = await _context.Factions.FindAsync(id);
        if (faction == null)
            throw new KeyNotFoundException("Event not found");
        return faction;
    }
}

