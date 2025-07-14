using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Organisations;

namespace Model.Entities.People;

[Table("TEACHER_BASED_lOCATION")]
public class TeacherLocation
{
    [Column("TEACHER_CODE")]
    public string TeacherCode { get; set; }
    
    public Teacher Teacher { get; set; }
    
    [Column("LOCATION_NAME")]
    public string LocationName { get; set; }
    
    public Location Location { get; set; }
}