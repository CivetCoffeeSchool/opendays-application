using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.OccupationUnits;
using Model.Entities.Organisations;
using Model.Entities.People;

namespace Model.Entities;

public class Assignment
{
    public APerson Person { get; set; }
    public Room Room { get; set; }
    public AOccupationUnit OccupationUnit { get; set; }
    
    [Column("PERSON_ID")]
    public string PersonId { get; set; }
    [Column("ROOM_NAME")]
    public string RoomName { get; set; }
    [Column("LOCATION")]
    public string Location { get; set; }
    [Column("OCCUPATION_UNIT_ID")]
    public int OccupationUnitId { get; set; }
    [Column("EVENT_NAME")]
    public string EventName { get; set; }
    
}