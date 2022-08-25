using System.ComponentModel.DataAnnotations;
using AnnotatedResult.DataAnnotations;

namespace AnnotatedResult.SampleTests.Models;

public class User
{
    [Required]
    public string Username { get; init; } = null!;

    [EmailAddress]
    public string Email { get; init; } = null!;

    [Required]
    [ValidateObject]
    public Profile Profile { get; init; } = new Profile();
}

public class Profile
{
    [Required]
    public string FirstName { get; init; } = null!;

    [Required]
    public string LastName { get; init; } = null!;

    [Required]
    [ValidateObject]
    public Address Address { get; set; } = new Address();
}

public class Address
{
    [Required] public string ZipCode { get; set; } = null!;
    [Required] public string City { get; set; } = null!;
}
