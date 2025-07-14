using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.EventRelated;
using Model.Entities.People;

namespace Model.Entities.OccupationUnits;

[Table("OCCUPATION_UNITS_BT")]
public abstract class AOccupationUnit
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }
    
    [Column("EVENT_NAME")]
    public string EventName { get; set; }
    public AEvent Event { get; set; }
    
    [Column("TEACHER_CODE")]
    public string TeacherCode { get; set; }
    
    public Teacher Teacher { get; set; }
    
    public List<Assignment> Assignments { get; set; }
}