using Domain.Repositories.DTOs;
using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configurations;
using Model.Entities;
using Model.Entities.EventRelated;
using Model.Entities.OccupationUnits;
using Model.Entities.Organisations;

namespace Domain.Repositories.Implementations;

public class EventRepository : ARepository<AEvent>, IEventRepository
{
    private readonly TdoTDbContext _dbContext;

    public EventRepository(TdoTDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public List<TrialGroupEvent> GetTrialGroupEvents()
    {
        return _dbContext.Events.OfType<TrialGroupEvent>()
            .Include(e => e.Location)
            .ToList();
    }

    public List<WorkshopEvent> GetWorkshopEvents()
    {
        return _dbContext.Events.OfType<WorkshopEvent>()
            .Include(e => e.Location)
            .ToList();
    }

    public List<AEvent> GetUpcomingEvents(DateTime fromDate)
    {
        return _dbContext.Events
            .Include(e => e.Location)
            .Where(e => e.StartDatetime >= fromDate)
            .OrderBy(e => e.StartDatetime)
            .ToList();
    }

    public AEvent? GetEventWithDetails(string name)
    {
        var eventItem = _dbContext.Events
            .Include(e => e.Location)
            .FirstOrDefault(e => e.Name == name);

        if (eventItem is TrialGroupEvent trialGroupEvent)
        {
            return _dbContext.Events.OfType<TrialGroupEvent>()
                .Include(e => e.Location)
                .FirstOrDefault(e => e.Name == name);
        }
        else if (eventItem is WorkshopEvent workshopEvent)
        {
            return _dbContext.Events.OfType<WorkshopEvent>()
                .Include(e => e.Location)
                .Include(e=>e.OccupationUnits)
                .FirstOrDefault(e => e.Name == name);
        }

        return eventItem;
    }

    public List<Assignment> GetAssignmentsForEvent(string eventName)
    {
        return _dbContext.Assignments
            .Include(a => a.Person)
            .Include(a => a.Room)
            .Include(a => a.OccupationUnit)
            .Where(a => a.EventName == eventName)
            .ToList();
    }

    public List<AOccupationUnit> GetOccupationUnitsForEvent(string eventName)
    {
        return _dbContext.OccupationUnits
            .Include(ou => ou.Assignments)
            .ThenInclude(a => a.Person)
            .Where(s => s.EventName == eventName)
            .ToList();
    }

    public Specialization? GetSpecializationForStation(int stationId)
    {
        return _dbContext.Stations
            .Include(s => s.Specialization)
            .Where(s => s.Id== stationId)
            .Select(s => s.Specialization)
            .FirstOrDefault();
    }

    public List<StationWithSpecializationDto> GetStationsWithSpecializations(string eventName)
    {
        return _dbContext.Stations
            .Include(s => s.Specialization)
            .Where(s => s.EventName == eventName)
            .Select(s => new StationWithSpecializationDto
            {
                StationId = s.Id,
                Name = s.Name,
                Description = s.Description,
                SpecializationName = s.Specialization.Name,
                SpecializationDescription = s.Specialization.Description
            })
            .ToList();
    }
}
//TODO
//check the best way to display specializations for workshops events