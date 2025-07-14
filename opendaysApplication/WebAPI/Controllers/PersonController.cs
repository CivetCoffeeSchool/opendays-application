using Domain.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model.Entities.People;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/people")]
public class PersonController: ControllerBase
{
    private readonly IRepository<APerson> _personRepository;
    private readonly ILogger<PersonController> _logger;
    
    public PersonController(IRepository<APerson> repository, ILogger<PersonController> logger)
    {
        _personRepository = repository;
        _logger = logger;
    }
    
    // GET: api/Person
    [HttpGet]
    public ActionResult<IEnumerable<APerson>> GetAllPersons()
    {
        try
        {
            var persons = _personRepository.ReadAll();
            return Ok(persons);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/Person/5
    [HttpGet("{id}")]
    public ActionResult<APerson> GetPersonById(string id)
    {
        try
        {
            // Note: Since your Read method expects an int, but Person ID is string,
            // you'll need to either modify the repository or add a string-based Read method
            // For now, I'm using the filter expression approach
            var person = _personRepository.Read(p => p.Code == id).FirstOrDefault();
            
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // POST: api/Person
    [HttpPost]
    public ActionResult<APerson> CreatePerson([FromBody] APerson person)
    {
        try
        {
            if (person == null)
            {
                return BadRequest("Person object is null");
            }

            var createdPerson = _personRepository.Create(person);
            return CreatedAtAction(nameof(GetPersonById), new { id = createdPerson.Code }, createdPerson);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // PUT: api/Person/5
    [HttpPut("{id}")]
    public IActionResult UpdatePerson(string id, [FromBody] APerson person)
    {
        try
        {
            if (person == null || id != person.Code)
            {
                return BadRequest("Invalid person data");
            }

            var existingPerson = _personRepository.Read(p => p.Code == id).FirstOrDefault();
            if (existingPerson == null)
            {
                return NotFound();
            }

            _personRepository.Update(person);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // DELETE: api/Person/5
    [HttpDelete("{id}")]
    public IActionResult DeletePerson(string id)
    {
        try
        {
            var person = _personRepository.Read(p => p.Code == id).FirstOrDefault();
            if (person == null)
            {
                return NotFound();
            }

            _personRepository.Delete(person);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/Person/search?name=John
    [HttpGet("search")]
    public ActionResult<IEnumerable<APerson>> SearchPersons([FromQuery] string name)
    {
        try
        {
            var persons = _personRepository.Read(p => 
                p.Forename.Contains(name) || 
                p.Familyname.Contains(name));
            
            return Ok(persons);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}