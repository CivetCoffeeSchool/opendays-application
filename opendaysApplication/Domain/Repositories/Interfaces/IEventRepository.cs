using Domain.Repositories.DTOs;
using Model.Entities;
using Model.Entities.EventRelated;
using Model.Entities.OccupationUnits;
using Model.Entities.Organisations;

namespace Domain.Repositories.Interfaces;

public interface IEventRepository: IRepository<AEvent>
{
    List<TrialGroupEvent> GetTrialGroupEvents();
    List<WorkshopEvent> GetWorkshopEvents();
    List<AEvent> GetUpcomingEvents(DateTime fromDate);
    AEvent? GetEventWithDetails(string name);
    List<Assignment> GetAssignmentsForEvent(string eventName);
    List<AOccupationUnit> GetOccupationUnitsForEvent(string eventName);
    
    Specialization? GetSpecializationForStation(int stationId);
    List<StationWithSpecializationDto> GetStationsWithSpecializations(string eventName);
}