using AnnotatedResult.SampleTests.Models;

namespace AnnotatedResult.SampleTests.Samples;

public static class MetadataTest
{
    public static void Run()
    {
        var metadata = new Dictionary<string, object>
        {
            { "DateTime", DateTime.UtcNow }
        };

        var result = Result.Ok();
        result.AddMetadata(metadata);
        result.Log();
    }
}
