using AnnotatedResult.SampleTests.Models;

namespace AnnotatedResult.SampleTests.Samples;

public static class Result400
{
    public static void Run()
    {
        var unauthorized = Result<Request>.Unauthorized();
        var forbidden = Result<Request>.Forbidden();
        var conflict = Result<Request>.Conflict();
        var notFound = Result<Request>.NotFound();

        unauthorized.Log();
        forbidden.Log();
        conflict.Log();
        notFound.Log();
    }
}
