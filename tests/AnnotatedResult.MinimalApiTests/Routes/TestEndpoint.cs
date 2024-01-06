using Microsoft.AspNetCore.Mvc;

namespace AnnotatedResult.MinimalApiTests.Routes;

public class TestEndpoint : IRouteEndpoint
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/", Test);
        app.MapGet("/error", ReturnError);
        app.MapPost("/register", Register);
    }

    internal IResult ReturnError(HttpContext httpContext)
    {
        var error = new Error("Something went wrong.", ErrorSeverity.Critical);
        var result = Result.Error(404, error);

        return result.Match(() => Results.Ok(),
            error => error.AsProblem(new ProblemDetails
            {
                Title = "Failed",
            },
            httpContext));
    }

    internal IResult Test(HttpContext httpContext)
    {
        var result = Result.Ok();

        result.Match(() => Console.WriteLine(result.Status),
            error => error.AsProblem(new ProblemDetails
            {
                Title = "Failed",
            },
            httpContext));

        return result.Match(() => Results.Ok(),
            error => error.AsProblem(new ProblemDetails
            {
                Title = "Failed",
            },
            httpContext));
    }

    internal IResult Register(Request request, HttpContext httpContext)
    {
        var result = Registration(request);
        return result.Match(
            value => Results.Ok(value),
            fault => fault.AsProblem(new ProblemDetails
            {
                Title = "Failed"
            },
            httpContext));
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
