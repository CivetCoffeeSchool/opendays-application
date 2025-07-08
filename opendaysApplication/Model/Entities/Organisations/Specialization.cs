using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Entities.Organisations;

public class Specialization
{
    [Key]
    [Column("NAME")]
    public string Name {get; set;}
    
    [Required]
    [Column("DESCRIPTION")]
    public string Description {get; set;}
    
}