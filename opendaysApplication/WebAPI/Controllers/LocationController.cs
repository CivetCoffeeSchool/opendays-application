using Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Entities.EventRelated;
using Model.Entities.Organisations;

namespace WebAPI.Controllers;

public class LocationController : ControllerBase
{
    private readonly ILocationRepository _locationRepository;

    public LocationController(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    // GET: api/Location
    [HttpGet]
    public ActionResult<IEnumerable<Location>> GetAllLocations()
    {
        try
        {
            var locations = _locationRepository.ReadAll();
            return Ok(locations);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/Location/MainCampus
    [HttpGet("{name}")]
    public ActionResult<Location> GetLocationByName(string name)
    {
        try
        {
            var location = _locationRepository.Read(name);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/Location/MainCampus/rooms
    [HttpGet("{name}/rooms")]
    public ActionResult<IEnumerable<Room>> GetRoomsForLocation(string name)
    {
        try
        {
            var rooms = _locationRepository.GetRoomsForLocation(name);
            return Ok(rooms);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/Location/MainCampus/events
    [HttpGet("{name}/events")]
    public ActionResult<IEnumerable<AEvent>> GetEventsForLocation(string name)
    {
        try
        {
            var events = _locationRepository.GetEventsForLocation(name);
            return Ok(events);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/Location/MainCampus/details
    [HttpGet("{name}/details")]
    public ActionResult<Location> GetLocationWithDetails(string name)
    {
        try
        {
            var location = _locationRepository.GetLocationWithDetails(name);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // POST: api/Location
    [HttpPost]
    public ActionResult<Location> CreateLocation([FromBody] Location location)
    {
        try
        {
            if (location == null)
            {
                return BadRequest("Location object is null");
            }

            var createdLocation = _locationRepository.Create(location);
            return CreatedAtAction(nameof(GetLocationByName), new { name = createdLocation.LocationName }, createdLocation);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // PUT: api/Location/MainCampus
    [HttpPut("{name}")]
    public IActionResult UpdateLocation(string name, [FromBody] Location location)
    {
        try
        {
            if (location == null || name != location.LocationName)
            {
                return BadRequest("Invalid location data");
            }

            var existingLocation = _locationRepository.Read(name);
            if (existingLocation == null)
            {
                return NotFound();
            }

            _locationRepository.Update(location);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // DELETE: api/Location/MainCampus
    [HttpDelete("{name}")]
    public IActionResult DeleteLocation(string name)
    {
        try
        {
            var location = _locationRepository.Read(name);
            if (location == null)
            {
                return NotFound();
            }

            _locationRepository.Delete(location);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}