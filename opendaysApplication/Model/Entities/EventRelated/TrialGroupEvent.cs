using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.OccupationUnits;

namespace Model.Entities.EventRelated;

public class TrialGroupEvent:AEvent
{
    [Column("DESCRIPTION")]
    public string? Description {get; set;}
}