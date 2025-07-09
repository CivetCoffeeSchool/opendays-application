using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configurations;
using Model.Entities.EventRelated;
using Model.Entities.Organisations;

namespace Domain.Repositories.Implementations;

public class LocationRepository : ARepository<Location>,ILocationRepository
{
    private readonly TdoTDbContext _dbContext;

    public LocationRepository(TdoTDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public List<Room> GetRoomsForLocation(string locationName)
    {
        return _dbContext.Rooms
            .Include(r => r.Location)
            .Where(r => r.LocationName == locationName)
            .ToList();
    }

    public List<AEvent> GetEventsForLocation(string locationName)
    {
        return _dbContext.Events
            .Include(e => e.Location)
            .Where(e => e.LocationName == locationName)
            .ToList();
    }

    public Location? GetLocationWithDetails(string locationName)
    {
        return _dbContext.Locations
            .Include(l => l.Rooms)
            .Include(l => l.Events)
            .ThenInclude(e => e.OccupationUnits)
            .FirstOrDefault(l => l.LocationName == locationName);
    }
}