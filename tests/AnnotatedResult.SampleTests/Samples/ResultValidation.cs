using AnnotatedResult.SampleTests.Models;

namespace AnnotatedResult.SampleTests.Samples;

public static class ResultValidation
{
    public static void Run()
    {
        var result = Result<Request>.Validate(new Request()
        {
            Username = "JohnWick",
            Email = "j.wick@continental",
            Password = "FortisFortunaAdiuvat"
        });

        Serilog.Log.Information("Status: {0}", result.Status);
        if(result.IsSuccess)
        {
            Request request = result;
            Serilog.Log.Information("Username: {0}", request.Username);
            Serilog.Log.Information("Email: {0}", request.Email);
            Serilog.Log.Information("Password: {0}", request.Password);
            return;
        }

        foreach(var error in result.Errors)
        {
            Serilog.Log.Information(error.Message);
        }
    }
}
