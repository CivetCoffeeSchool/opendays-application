using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Entities.People;

namespace Model.Entities.Users;

public abstract class AUser
{
    [Key]
    [Column("USERNAME")]
    public string Username { get; set; }
    
    [Column("PASSWORD_HASH")]
    public string PasswordHash { get; set; }
    
    public APerson Person { get; set; }
    
    [Column("PERSON_ID")]
    public string PersonId { get; set; }
}