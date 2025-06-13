using AnnotatedResult.SampleTests.Models;

namespace AnnotatedResult.SampleTests.Samples;

public static class DefaultEmptyResult
{
    public static void Run()
    {
        var result1 = Result.CreateEmpty();
        var result2 = Result.CreateEmpty("there's nothing inside, empty result");
        var result3 = Result<Request>.CreateEmpty();
        var result4 = Result<Request>.CreateEmpty("there's nothing inside, empty result");

        result1.Log();
        result2.Log();
        result3.Log();
        result4.Log();
    }
}
