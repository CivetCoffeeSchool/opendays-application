using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.EventRelated;
using Model.Entities.Organisations;

namespace Model.Entities.OccupationUnits;

public class Station: AOccupationUnit
{

    [Required]
    [Column("NAME")]
    public string Name { get; set; }

    [Column("DESCRIPTION")]
    public string? Description { get; set; }

    public Specialization Specialization { get; set; }
    
    [Column("SPECIALIZATION_NAME")]
    public string SpecializationName { get; set; }
    
}