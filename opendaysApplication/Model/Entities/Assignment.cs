using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.OccupationUnits;
using Model.Entities.Organisations;
using Model.Entities.People;

namespace Model.Entities;

[Table("ASSIGNMENTS_JT")]
public class Assignment
{
    
    [Column("STUDENT_CODE")]
    public string StudentCode { get; set; }
    
    public Student Student { get; set; }
    
    [Column("OCCUPATION_UNIT_ID")]
    public int OccupationUnitId { get; set; }
    
    public AOccupationUnit OccupationUnit { get; set; }
    [Column("EVENT_NAME")]
    public string EventName { get; set; }
    
    [Column("IS_ASSIGNED")]
    public bool IsAssigned { get; set; }
    
}