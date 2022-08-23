using AnnotatedResult.SampleTests.Models;

namespace AnnotatedResult.SampleTests.Samples;

public static class ResultValidation
{
    public static void Run()
    {
        Serilog.Log.Information("Starting...");

        var result = Result.Validate(new Request()
        {
            Username = "John Wick",
            Email = "john.wick@continental",
            Password = "FortisFortunaAdiuvat"
        });

        if(result.IsSuccess)
        {
            Request request = result;
            Serilog.Log.Information("Status: {0}", result.Status);
            Serilog.Log.Information("Result: {0}", request.Email);
            return;
        }

        foreach(var errorMessage in result.Errors)
        {
            Serilog.Log.Information(errorMessage);
        }
    }
}
