using System.ComponentModel.DataAnnotations;

namespace AnnotatedResult.SampleTests.Models;

public class Request
{
    [Required(ErrorMessage = "Username cannot be empty.")]
    public string Username { get; } = null!;

    [Required]
    public string Email { get; } = null!;

    [Required]
    public string Password { get; } = null!;
}
