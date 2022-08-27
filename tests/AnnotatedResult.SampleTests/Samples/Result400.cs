using AnnotatedResult.SampleTests.Models;

namespace AnnotatedResult.SampleTests.Samples;

public static class ErrorTemplate
{
    public static Error Error404 => new("404 Not Found.", ErrorSeverity.Error);
}

public static class Result400
{
    public static void Run()
    {
        var unauthorized = Result<Request>.Unauthorized();
        var forbidden = Result<Request>.Forbidden();
        var conflict = Result<Request>.Conflict();
        var notFound = Result<Request>.NotFound(ErrorTemplate.Error404);

        unauthorized.Log();
        forbidden.Log();
        conflict.Log();
        notFound.Log();
    }
}
