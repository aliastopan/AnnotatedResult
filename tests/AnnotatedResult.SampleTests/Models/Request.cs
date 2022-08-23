using System.ComponentModel.DataAnnotations;

namespace AnnotatedResult.SampleTests.Models;

public class Request
{
    [Required(ErrorMessage = "Username cannot be empty.")]
    public string Username { get; init; } = null!;

    [Required]
    public string Email { get; init; } = null!;

    [Required]
    public string Password { get; init; } = null!;
}
