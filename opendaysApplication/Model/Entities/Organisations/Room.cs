using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.OccupationUnits;

namespace Model.Entities.Organisations;
[Table("ROOMS")]
public class Room
{
    [Key]
    [Column("NAME")]
    public string Name { get; set; }
    
    [Column("DESCRIPTION")]
    public string? Description { get; set; }

    [Column("MAX_CAPACITY")]
    public int? MaxCapacity { get; set; }
    

    [Column("LOCATION")]
    public string LocationName { get; set; }

    public Location Location { get; set; }
    
    public List<Station> Stations { get; set; }= new();
}