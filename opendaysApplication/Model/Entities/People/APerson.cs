using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.Users;

namespace Model.Entities.People;
[Table("PERSON_BT")]
public abstract class APerson
{
    [Key]
    [Column("CODE"),StringLength(10)]
    public string Code { get; set; }

    [Required]
    [Column("FORENAME"), StringLength(50)]
    public string Forename { get; set; }

    [Required]
    [Column("FAMILYNAME"), StringLength(50)]
    public string Familyname { get; set; }

    [Required]
    [Column("GENDER"), StringLength(1)]
    public string Gender { get; set; }
    
    public AUser User { get; set; }
}