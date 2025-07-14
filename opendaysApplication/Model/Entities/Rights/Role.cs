using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Rights;
[Table("E_ROLES")]
public class Role
{
    [Key][Column("CODE")]
    public string Code { get; set; }
    
    public List<Right> Rights { get; set; } = new List<Right>();
}