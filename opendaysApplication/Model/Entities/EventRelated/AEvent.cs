using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.OccupationUnits;
using Model.Entities.Organisations;

namespace Model.Entities.EventRelated;

[Table("EVENTS_BT")]
public abstract class AEvent
{
    [Key]
    [Column("NAME")]
    public string Name { get; set; } = null!;

    [Required]
    [Column("START_DATETIME")]
    public DateTime StartDatetime { get; set; }

    [Required]
    [Column("END_TIME")]
    public TimeOnly EndTime { get; set; }


    [Required]
    [Column("STATUS")]
    public string Status { get; set; }
    //Aktiv oder Archiviert

    
    
    //1-optional
    [Column("COPY_OF")]
    public string? CopyOf { get; set; }
    
    public AEvent? EventCopiedFrom { get; set; }
    
    public List<AEvent> CopiedEvents { get; set; }
    
    
    [Column("LOCATION_NAME")]
    public string LocationName { get; set; }
    
    public Location Location { get; set; }

    
    public List<AOccupationUnit> OccupationUnits { get; set; }
    
}