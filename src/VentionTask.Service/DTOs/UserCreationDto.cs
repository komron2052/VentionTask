using System.ComponentModel.DataAnnotations;

namespace Ventiontask.Service.DTOs;

public class UserCreationDto
{
    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Phone is required")]
    [Phone]
    public string Phone { get; set; }

   [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    public long GroupId { get; set; }
}
