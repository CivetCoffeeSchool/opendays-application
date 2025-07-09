using Domain.Repositories.DTOs;
using Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Model.Entities.EventRelated;
using Model.Entities.OccupationUnits;
using Model.Entities.Organisations;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventRepository _eventRepository;

    public EventController(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    // GET: api/Event
    [HttpGet]
    public ActionResult<IEnumerable<AEvent>> GetAllEvents()
    {
        try
        {
            var events = _eventRepository.ReadAll();
            return Ok(events);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/Event/trial-group
    [HttpGet("trial-group")]
    public ActionResult<IEnumerable<TrialGroupEvent>> GetTrialGroupEvents()
    {
        try
        {
            var events = _eventRepository.GetTrialGroupEvents();
            return Ok(events);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/Event/workshop
    [HttpGet("workshop")]
    public ActionResult<IEnumerable<WorkshopEvent>> GetWorkshopEvents()
    {
        try
        {
            var events = _eventRepository.GetWorkshopEvents();
            return Ok(events);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/Event/upcoming
    [HttpGet("upcoming")]
    public ActionResult<IEnumerable<AEvent>> GetUpcomingEvents()
    {
        try
        {
            var events = _eventRepository.GetUpcomingEvents(DateTime.Now);
            return Ok(events);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/Event/{name}
    [HttpGet("{name}")]
    public ActionResult<AEvent> GetEventByName(string name)
    {
        try
        {
            var eventItem = _eventRepository.Read(name);
            if (eventItem == null)
            {
                return NotFound();
            }
            return Ok(eventItem);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/Event/{name}/details
    [HttpGet("{name}/details")]
    public ActionResult<AEvent> GetEventWithDetails(string name)
    {
        try
        {
            var eventDetails = _eventRepository.GetEventWithDetails(name);
            if (eventDetails == null)
            {
                return NotFound();
            }
            return Ok(eventDetails);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/Event/{name}/assignments
    [HttpGet("{name}/assignments")]
    public ActionResult<IEnumerable<Assignment>> GetAssignmentsForEvent(string name)
    {
        try
        {
            var assignments = _eventRepository.GetAssignmentsForEvent(name);
            return Ok(assignments);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/Event/{name}/stations
    [HttpGet("{name}/stations")]
    public ActionResult<IEnumerable<Station>> GetGetOccupationUnitsForEvent(string name)
    {
        try
        {
            var stations = _eventRepository.GetOccupationUnitsForEvent(name);
            return Ok(stations);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // POST: api/Event
    [HttpPost]
    public ActionResult<AEvent> CreateEvent([FromBody] AEvent eventItem)
    {
        try
        {
            if (eventItem == null)
            {
                return BadRequest("Event object is null");
            }

            var createdEvent = _eventRepository.Create(eventItem);
            return CreatedAtAction(nameof(GetEventByName), new { name = createdEvent.Name }, createdEvent);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // PUT: api/Event/{name}
    [HttpPut("{name}")]
    public IActionResult UpdateEvent(string name, [FromBody] AEvent eventItem)
    {
        try
        {
            if (eventItem == null || name != eventItem.Name)
            {
                return BadRequest("Invalid event data");
            }

            var existingEvent = _eventRepository.Read(name);
            if (existingEvent == null)
            {
                return NotFound();
            }

            _eventRepository.Update(eventItem);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // DELETE: api/Event/{name}
    [HttpDelete("{name}")]
    public IActionResult DeleteEvent(string name)
    {
        try
        {
            var eventItem = _eventRepository.Read(name);
            if (eventItem == null)
            {
                return NotFound();
            }

            _eventRepository.Delete(eventItem);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
        
    }
    
    // GET: api/Event/station/5/specialization
    [HttpGet("station/{stationId}/specialization")]
    public ActionResult<Specialization> GetSpecializationForStation(int stationId)
    {
        try
        {
            var specialization = _eventRepository.GetSpecializationForStation(stationId);
            if (specialization == null)
            {
                return NotFound($"Specialization not found for station with ID {stationId}");
            }
            return Ok(specialization);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/Event/name/stations-with-specializations
    [HttpGet("{eventName}/stations-with-specializations")]
    public ActionResult<List<StationWithSpecializationDto>> GetStationsWithSpecializations(string eventName)
    {
        try
        {
            var stations = _eventRepository.GetStationsWithSpecializations(eventName);
            return Ok(stations);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}