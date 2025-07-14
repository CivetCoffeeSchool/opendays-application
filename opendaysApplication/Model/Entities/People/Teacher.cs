using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.OccupationUnits;

namespace Model.Entities.People;

[Table("TEACHERS")]
public class Teacher:APerson
{
    public List<AOccupationUnit> OccupationUnits { get; set; } = new();
    public List<TeacherLocation> TeacherLocations { get; set; } = new();
}