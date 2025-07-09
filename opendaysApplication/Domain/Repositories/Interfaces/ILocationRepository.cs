using Model.Entities.EventRelated;
using Model.Entities.Organisations;

namespace Domain.Repositories.Interfaces;

public interface ILocationRepository: IRepository<Location>
{
    List<Room> GetRoomsForLocation(string locationName);
    List<AEvent> GetEventsForLocation(string locationName);
    Location? GetLocationWithDetails(string locationName);
}