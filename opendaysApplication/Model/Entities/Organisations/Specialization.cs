using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.OccupationUnits;

namespace Model.Entities.Organisations;
[Table("SPECIALIZATIONS")]
public class Specialization
{
    [Key]
    [Column("NAME")]
    public string Name {get; set;}
    
    [Required]
    [Column("DESCRIPTION")]
    public string Description {get; set;}
    
    public List<Station> Stations { get; set; } = new();
    
}