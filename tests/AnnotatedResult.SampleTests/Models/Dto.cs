using System.ComponentModel.DataAnnotations;

namespace AnnotatedResult.SampleTests.Models;

public class Dto
{
    [Required]
    public string Username { get; init; } = null!;

    [Required, MinLength(8)]
    public string Password { get; init; } = null!;
}
