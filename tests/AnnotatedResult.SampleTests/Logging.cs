namespace AnnotatedResult.SampleTests;

public static class Logging
{
    public static void Log(this Result result)
    {
        if(result.IsSuccess)
            return;

        Serilog.Log.Information("Status: {0}", result.Status);
        foreach(var error in result.Errors)
        {
            Serilog.Log.Information("[{0}] {1}",
                error.Severity,
                error.Message);
        }
    }
}
