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

    internal IResult Register(Request request, HttpContext httpContext)
    {
        var result = Registration(request);
        return result.Match(
            value => Results.Ok(value),
            fault => Results.Problem(new ProblemDetails
            {
                Title = "Registration failed.",
                Status = (int)fault.status
            }));
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
