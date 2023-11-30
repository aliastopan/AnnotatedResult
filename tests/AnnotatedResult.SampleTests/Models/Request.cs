#nullable disable

using System.ComponentModel.DataAnnotations;
using AnnotatedResult.SampleTests.Validations;

namespace AnnotatedResult.SampleTests.Models;

public class Request
{
    [Required]
    [RegularExpression(RegexPattern.Username)]
    public string Username { get; init; }

    [EmailAddress]
    public string Email { get; init; }

    [Required]
    [RegularExpression(RegexPattern.StrongPassword)]
    public string Password { get; init; }
}
