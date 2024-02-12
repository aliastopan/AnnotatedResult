using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AnnotatedResult.MinimalApiTests.Routes;

public class TestEndpoint2 : IRouteEndpoint
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/problem-details-ext", Ext);
    }

    internal IResult Ext(HttpContext httpContext)
    {
        string jsonString = "{\"type\":\"/error/test\",\"title\":\"Failed\",\"status\":404,\"instance\":\"/error\",\"traceId\":\"00-9f069df1e1bf9b5778c4d1bfc9bcf82f-76bee8c72132dae6-00\",\"errors\":[{\"message\":\"Something went wrong.\",\"severity\":\"Critical\"}]}";

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(jsonString, options);

        Console.WriteLine($"Problem Details Instance: {problemDetails!.Instance}");
        Console.WriteLine($"Problem Details Type: {problemDetails!.Type}");
        Console.WriteLine($"Problem Details Status: {problemDetails!.Status}");
        Console.WriteLine($"Problem Details Extension: {problemDetails!.Extensions["errors"]}");

        var extension = problemDetails.GetExtension();
        if (extension is not null)
        {
            Console.WriteLine(extension.Errors.Count);
            Console.WriteLine($"Error Message: {extension.Errors.First().Message}");
            Console.WriteLine($"Error Severity: {extension.Errors.First().Severity}");
        }

        return Results.Ok();
    }
}


