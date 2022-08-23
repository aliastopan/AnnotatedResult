namespace AnnotatedResult.MinimalApiTests.Routes;

public class TestEndpoint : IRouteEndpoint
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/", Test);
    }

    internal IResult Test()
    {
        return Results.Ok();
    }
}
