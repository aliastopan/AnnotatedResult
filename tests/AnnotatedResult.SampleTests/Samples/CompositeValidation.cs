using AnnotatedResult.SampleTests.Models;

namespace AnnotatedResult.SampleTests.Samples;

public static class CompositeValidation
{
    public static void Run()
    {
        var result = Result<User>.Validate(new User
        {
            Username = "JohnWick",
            Email = "john.wick@continental",
            Profile = new Profile
            {
                FirstName = "Jardani",
                LastName = "Jovonovich",
                Address = new Address
                {
                    ZipCode = "10001",
                    City = "New York"
                }
            }
        });

        Serilog.Log.Information("Status: {0}", result.Status);
        if(result.IsSuccess)
        {
            User request = result;
            Serilog.Log.Information("Username: {0}", request.Username);
            Serilog.Log.Information("Email: {0}", request.Email);
            Serilog.Log.Information("FirstName: {0}", request.Profile.FirstName);
            Serilog.Log.Information("LastName: {0}", request.Profile.LastName);
            return;
        }

        result.Log();
    }
}
