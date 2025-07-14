using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.OccupationUnits;

namespace Model.Entities.EventRelated;

[Table("TRIAL_GROUP_EVENTS")]
public class TrialGroupEvent:AEvent
{
    [Column("DESCRIPTION")]
    public string? Description {get; set;}
    
    [Column("MAX_CAPACITY")]
    public int MaxCapacity {get; set;}
}