using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.EventRelated;

namespace Model.Entities.OccupationUnits;

public abstract class AOccupationUnit
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public AEvent Event { get; set; }
    
    [Column("EVENT_NAME")]
    public string EventName { get; set; }
    
    public List<Assignment> Assignments { get; set; }
}