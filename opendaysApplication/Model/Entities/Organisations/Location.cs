using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.EventRelated;
using Model.Entities.People;

namespace Model.Entities.Organisations;

[Table("LOCATIONS")]
public class Location
{
    [Key] [Column("LOCATION_NAME")] 
    public string LocationName { get; set; }

    [Required]
    [Column("ADDRESS")]
    public string Address { get; set; }

    [Column("CONTACT_INFORMATION")]
    public string? ContactInformation { get; set; }

    // Navigation properties
    public List<Room> Rooms { get; set; } = new();
    public List<AEvent> Events { get; set; } = new();
    
    public List<TeacherLocation> LocationTeachers { get; set; } = new();
}
