
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.EventRelated;

namespace Model.Entities.OccupationUnits;

[Table("CLASS_PERIODS")]
public class ClassPeriod: AOccupationUnit
{
    [Required]
    [Column("PERIOD_TIME_NUMBER")]
    public int PeriodTimeNumber {get; set;}
    
    
}