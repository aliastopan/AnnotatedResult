using AnnotatedResult.SampleTests.Samples;

namespace AnnotatedResult.SampleTests;

public static class Sampling
{
    public static void Run()
    {
        Serilog.Log.Information("Starting...");
        // ResultValidation.Run();
        // CompositeValidation.Run();
        // Result400.Run();
        // CustomError.Run();
        // CompareValidation.Run();
        MetadataTest.Run();
    }
}
