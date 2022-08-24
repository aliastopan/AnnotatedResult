using AnnotatedResult.SampleTests.Samples;

namespace AnnotatedResult.SampleTests;

public static class Sampling
{
    public static void Run()
    {
        Serilog.Log.Information("Starting...");
        // CustomValidation.Run();
        // ResultValidation.Run();
        Result400.Run();
        CustomError.Run();
    }
}
