using Model.Entities;
using Model.Entities.EventRelated;
using Model.Entities.OccupationUnits;

namespace Domain.Repositories.Interfaces;

public interface IEventRepository: IRepository<AEvent>
{
    List<AEvent> GetEventsByType(string eventType);
    List<AEvent> GetUpcomingEvents(DateTime fromDate);
    AEvent? GetEventWithDetails(int id);
    List<Assignment> GetAssignmentsForEvent(int eventId);
    List<Station> GetStationsForEvent(int eventId);
}