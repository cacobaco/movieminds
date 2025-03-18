using System.ComponentModel.DataAnnotations;
using Movieminds.Presentation.Enums;

namespace Movieminds.Presentation.Requests.Authentication;

public class RegisterRequest
{
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [EnumDataType(typeof(Gender))]
    public Gender? Gender { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateOnly? BirthDate { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    [RegularExpression(@"^[a-zA-Z0-9_.]+$")]
    public string Username { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(30)]
    public string Password { get; set; }

    [Required]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}
