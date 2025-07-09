using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Configurations;
using Model.Entities;
using Model.Entities.People;

namespace Domain.Repositories.Implementations;

public class PersonRepository : ARepository<APerson>, IPersonRepository
{
    private readonly TdoTDbContext _dbContext;
    
    public PersonRepository(TdoTDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public List<Assignment> GetAssignmentsForStudent(string studentId)
    {
        return _dbContext.Assignments
            .Include(a => a.OccupationUnit)
            .ThenInclude(ou => ou.Event)
            .Include(a => a.Room)
            .Where(a => a.PersonId == studentId)
            .ToList();
    }

    public Class? GetClassForStudent(string studentId)
    {
        return _dbContext.Students
            .Include(s => s.Class)
            .Where(s => s.Id == studentId)
            .Select(s => s.Class)
            .FirstOrDefault();
    }

    public Student? GetStudentWithDetails(string studentId)
    {
        return _dbContext.Students
            .Include(s => s.Class)
            .Include(s => s.Assignments)
            .ThenInclude(a => a.OccupationUnit)
            .ThenInclude(ou => ou.Event)
            .Include(s => s.Assignments)
            .ThenInclude(a => a.Room)
            .FirstOrDefault(s => s.Id == studentId);
    }
}