using System.ComponentModel.DataAnnotations;

namespace AnnotatedResult.SampleTests.Models;

public class ResetPassword
{
    [Required]
    public string NewPassword { get; init; } = null!;

    [Required]
    [Compare(nameof(NewPassword))]
    public string RepeatPassword { get; init; } = null!;
}
