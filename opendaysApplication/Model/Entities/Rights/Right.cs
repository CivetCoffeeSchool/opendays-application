using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Rights;

[Table("RIGHTS_JT")]
public class Right
{
    [Column("E_ROLE_CODE")]
    public string RoleCode { get; set; }
    public Role Role { get; set; }
    
    [Column("E_FUNCTIONALITY_CODE")]
    public string FunctionalityCode { get; set; }
    public Functionality Functionality { get; set; }
}