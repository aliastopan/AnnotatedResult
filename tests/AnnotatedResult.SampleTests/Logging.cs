namespace AnnotatedResult.SampleTests;

public static class Logging
{
    public static void Log(this Result result)
    {
        if(result.IsSuccess && result.HasMetadata)
        {
            foreach(var metadata in result.Metadata)
            {
                Serilog.Log.Information("[{0}] {1}", metadata.Key, metadata.Value);
            }

            return;
        }

        Serilog.Log.Information("Status: {0}", result.Status);
        foreach(var error in result.Errors)
        {
            Serilog.Log.Information("[{0}] {1}", error.Severity, error.Message);
        }
    }
}
