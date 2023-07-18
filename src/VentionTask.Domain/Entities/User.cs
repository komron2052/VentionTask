using System.ComponentModel.DataAnnotations;
using Ventiontask.Domain.Commons;

namespace Ventiontask.Domain.Entities;

public class User : Auditable
{
    [MinLength(3), MaxLength(50)]
    public string FirstName { get; set; }

    [MinLength(3), MaxLength(50)]
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public long GroupId { get; set; }
    public Group Group { get; set; }
}