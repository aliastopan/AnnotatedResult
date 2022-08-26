using System.ComponentModel.DataAnnotations;

namespace AnnotatedResult.UnitTests.Validator;

public class Request
{
    [Required] public string Username { get; init; } = null!;
    [Required] public string Password { get; init; } = null!;
}

public class InternalValidatorTests
{
    [Fact]
    public void ValidResultTest()
    {
        var validator = new InternalValidator();
        var goodRequest = new Request
        {
            Username = "user.name",
            Password = "longpassword"
        };
        var result = validator.TryValidate<Request>(goodRequest, out _);
        Assert.True(result);
    }

    [Fact]
    public void InvalidResultTest()
    {
        var validator = new InternalValidator();
        var badRequest = new Request
        {
            Username = "user.name",
        };
        var result = validator.TryValidate<Request>(badRequest, out _);
        Assert.False(result);
    }
}
