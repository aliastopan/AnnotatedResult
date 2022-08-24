using AnnotatedResult.SampleTests.Models;

namespace AnnotatedResult.SampleTests.Samples;

public static class CustomError
{
    public static void Run()
    {
        var error = new Error("Internal Server Error", ErrorSeverity.Error);
        var result = Result<Request>.Error(500, error);
        result.Log();
    }
}
