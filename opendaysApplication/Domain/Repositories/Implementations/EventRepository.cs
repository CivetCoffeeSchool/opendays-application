using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configurations;
using Model.Entities;
using Model.Entities.EventRelated;
using Model.Entities.OccupationUnits;

namespace Domain.Repositories.Implementations;

public class EventRepository : ARepository<AEvent>, IEventRepository
{
    private readonly TdoTDbContext _dbContext;

    public EventRepository(TdoTDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public List<AEvent> GetEventsByType(string eventType)
    {
        return _dbContext.Events
            .Where(e => e.EventType == eventType)
            .ToList();
    }

    public List<AEvent> GetUpcomingEvents(DateTime fromDate)
    {
        return _dbContext.Events
            .Where(e => e.StartDate >= fromDate)
            .OrderBy(e => e.StartDate)
            .ToList();
    }

    public AEvent? GetEventWithDetails(string name)
    {
        return _dbContext.Events
            .Include(e => e.Location)
            .Include(e => e.OccupationUnits)
            .ThenInclude(e => e.Assignments)
            .ThenInclude(a => a.Person)
            .FirstOrDefault(e => e.Name == name);
    }

    public List<Assignment> GetAssignmentsForEvent(int eventId)
    {
        return _dbContext.Assignments
            .Include(a => a.Person)
            .Include(a => a.Room)
            .Include(a => a.OccupationUnit)
            .Where(a => a.OccupationUnit.EventId == eventId)
            .ToList();
    }

    public List<Station> GetStationsForEvent(int eventId)
    {
        return _dbContext.Stations
            .Include(s => s.Specialization)
            .Include(s => s.OccupationUnit)
            .ThenInclude(ou => ou.Assignments)
            .ThenInclude(a => a.Person)
            .Where(s => s.OccupationUnit.EventId == eventId)
            .ToList();
    }
}