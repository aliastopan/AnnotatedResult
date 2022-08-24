using System.ComponentModel.DataAnnotations;

namespace AnnotatedResult.SampleTests.Models;

public class Request
{
    [Required]
    public string Username { get; init; } = null!;

    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;

    [EmailAddress]
    public string Email { get; init; } = null!;

    [Required]
    public string Password { get; init; } = null!;
}
