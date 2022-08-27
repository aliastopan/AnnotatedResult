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
        if(result.IsSuccess)
        {
            Response response = result;
            return Results.Ok(response);
        }
        else
        {
            var detail = new ProblemDetails
            {
                Status = (int)result.Status,
            };
            detail.Extensions["errors"] = result.Errors;
            return Results.Problem(detail);
        }
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
