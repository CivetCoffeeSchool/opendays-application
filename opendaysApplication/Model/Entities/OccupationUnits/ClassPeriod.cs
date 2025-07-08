
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.EventRelated;

namespace Model.Entities.OccupationUnits;

public class ClassPeriod: AOccupationUnit
{
    [Required]
    [Column("PERIOD_TIME_NUMBER")]
    public int PeriodTimeNumber {get; set;}
    
    
}