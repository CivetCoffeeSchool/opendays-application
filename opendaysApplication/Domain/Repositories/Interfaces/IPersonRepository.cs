using Model.Entities;
using Model.Entities.People;

namespace Domain.Repositories.Interfaces;

public interface IPersonRepository:IRepository<APerson>
{
    List<Assignment> GetAssignmentsForStudent(string studentId);
    Class? GetClassForStudent(string studentId);
    Student? GetStudentWithDetails(string studentId);
}