using AnnotatedResult.SampleTests.Models;

namespace AnnotatedResult.SampleTests.Samples;

public static class CompositeValidation
{
    public static void Run()
    {
        var user = new User
        {
            // Username = "JohnWick",
            // Email = "john.wick@continental",
            Profile = new Profile
            {
                // FirstName = "Jardani",
                // LastName = "Jovonovich",
                Address = new Address
                {
                    // ZipCode = "10001",
                    // City = "New York"
                }
            }
        };

        Result<User> result;
        var isValid = user.TryValidate(out var errors);
        if(!isValid)
        {
            result = Result<User>.Invalid(errors);
            result.Log();
            return;
        }

        result = Result<User>.Ok(user);
        Serilog.Log.Information("Status: {0}", result.Status);
        user = result;
        Serilog.Log.Information("Username: {0}", user.Username);
        Serilog.Log.Information("Email: {0}", user.Email);
        Serilog.Log.Information("FirstName: {0}", user.Profile.FirstName);
        Serilog.Log.Information("LastName: {0}", user.Profile.LastName);
    }
}
