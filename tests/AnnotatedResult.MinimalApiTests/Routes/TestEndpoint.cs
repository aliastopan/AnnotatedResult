using AnnotatedResult.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace AnnotatedResult.MinimalApiTests.Routes;

public class TestEndpoint : IRouteEndpoint
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/", Test);
        app.MapPost("/register", Register);
    }

    internal IResult Test()
    {
        return Results.Ok();
    }

    internal IResult Register(Request request)
    {
        var result = Registration(request);
        return result.HttpResult();
    }

    internal static Result<Response> Registration(Request request)
    {
        var isValid = request.TryValidate(out Error[] errors);
        if(!isValid)
        {
            return Result<Response>.Invalid(errors);
        }

        var response = new Response(
            Guid.NewGuid(),
            "user.name");

        return Result<Response>.Ok(response);
    }
}
