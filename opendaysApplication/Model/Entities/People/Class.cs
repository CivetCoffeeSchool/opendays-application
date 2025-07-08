using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.People;

public class Class
{
    [Key]
    [Column("CLASS_NAME")]
    public string ClassName { get; set; } 

    [Key]
    [Column("CURRENT_SCHOOLYEAR_START")]
    public int CurrentSchoolyearStart { get; set; } 
    //Uses the starting Year of a schoolyear
    //eg.: 2024/2025 ==> 2024
    
    public List<Student> Students { get; set; } = new();
}