namespace Application.Services;

using Application.Helpers;
using Application.Models.Activity;
using Application.Models.Faction;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFactionService
{
    Task<IEnumerable<Faction>> GetAll(int activityId);
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

    public async Task<IEnumerable<Faction>> GetAll(int activityId)
    {
        return await _context.Factions.Where(f => f.Activity.Id == activityId).ToListAsync();
    }

    public async Task<Faction> GetById(int id)
    {
        Faction? faction = await GetFactionById(id);
        return faction;
    }

    public void Post(FactionPostRequest model)
    {
        // validation

        var faction = _mapper.Map<Faction>(model);

        _context.Factions.Add(faction);
        _context.SaveChanges();
    }

    public async Task Delete(int id)
    {
        var faction = await GetFactionById(id);
        _context.Factions.Remove(faction);
        _context.SaveChanges();
    }

    public async Task Update(int id, FactionUpdateRequest model)
    {
        var faction = await GetFactionById(id);

        _mapper.Map(model, faction);
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

