using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.EventRelated;
using Model.Entities.Organisations;

namespace Model.Entities.OccupationUnits;
[Table("STATIONS")]
public class Station: AOccupationUnit
{

    [Required]
    [Column("NAME")]
    public string Name { get; set; }

    [Column("DESCRIPTION")]
    public string? Description { get; set; }

    
    [Column("SPECIALIZATION_NAME")]
    public string? SpecializationName { get; set; }
    
    public Specialization? Specialization { get; set; }
    
    [Column("ROOM_NAME")]
    
    public string RoomName { get; set; }
    
    [Column("ROOM_LOCATION")]
    
    public string RoomLocation { get; set; }
    
    public Room Room { get; set; }
    
    
    
}