using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.People;

public class Student: APerson
{
    [Required]
    [Column("BIRTHDAY")]
    public DateOnly Birthday { get; set; }

    
    
    
    [Column("CLASS_NAME")]
    public string ClassName { get; set; }
    
    [Column("CURRENT_SCHOOLYEAR_START")]
    public int CurrentSchoolyearStart { get; set; }
    
    public Class Class { get; set; }
}