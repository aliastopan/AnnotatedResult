using AnnotatedResult.SampleTests.Models;

namespace AnnotatedResult.SampleTests.Samples;

public static class ResultValidation
{
    public static void Run()
    {
        Serilog.Log.Information("Starting...");

        var request = new Request();
        var result = Result.Validate(request);

        if(result.IsSuccess)
        {
            Serilog.Log.Information("Status: {0}", result.Status);
            return;
        }

        foreach(var errorMessage in result.Errors)
        {
            Serilog.Log.Information(errorMessage);
        }
    }
}
