using System.ComponentModel.DataAnnotations;

namespace Movieminds.Presentation.Requests.Authentication;

public class LoginRequest
{
    [Required]
    public string Identifier { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
