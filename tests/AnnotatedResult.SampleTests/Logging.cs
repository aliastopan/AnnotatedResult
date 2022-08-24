namespace AnnotatedResult.SampleTests;

public static class Logging
{
    public static void Log(this Result result)
    {
        if(!result.IsSuccess)
        {
            Serilog.Log.Information("Status: {0} [{1}] {2}",
                result.Status,
                result.Errors[0].Severity,
                result.Errors[0].Message);
        }
    }
}
