namespace Application.Services;
using Application.Models.Faction;
using AutoMapper;
using Domain;
using Persistence;
using System.Threading.Tasks;

public interface IFactionService
{
    Task<Faction> GetById(int id);
    Task Post(FactionPostRequest model);
    Task Delete(int id);
    Task Update(int id, FactionUpdateRequest model);
}

public class FactionService : BaseService, IFactionService
{
    private readonly IMapper _mapper;

    public FactionService(DataContext context, IMapper mapper) :base(context)
    {
        _mapper = mapper;
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
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var faction = await GetFactionById(id);

        faction.IsDeleted = true;

        _context.Factions.Update(faction);
        await _context.SaveChangesAsync();
    }

    public async Task Update(int id, FactionUpdateRequest model)
    {
        var faction = await GetFactionById(id);
        var activity = await GetActivityById(model.ActivityId);

        _mapper.Map(model, faction);
        faction.Activity = activity;

        _context.Factions.Update(faction);
        await _context.SaveChangesAsync();
    }
}

