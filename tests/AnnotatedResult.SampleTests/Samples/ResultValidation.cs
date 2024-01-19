using AnnotatedResult.SampleTests.Models;

namespace AnnotatedResult.SampleTests.Samples;

public static class ResultValidation
{
    public static void Run()
    {
        var request = new Request()
        {
            Username = "JohnWick",
            Email = "j.wick@continental",
            Password = "FortisFortunaAdiuvat"
        };

        Result<Request> result;
        var isValid = request.TryValidate(out var errors);
        if(!isValid)
        {
            result = Result<Request>.Invalid(errors);
            result.Log();
            return;
        }

        result = Result<Request>.Ok(request);
        Serilog.Log.Information("Status: {0}", result.Status);
        if(result.IsSuccess())
        {
            request = result;
            Serilog.Log.Information("Username: {0}", request.Username);
            Serilog.Log.Information("Email: {0}", request.Email);
            Serilog.Log.Information("Password: {0}", request.Password);
            return;
        }
    }
}
