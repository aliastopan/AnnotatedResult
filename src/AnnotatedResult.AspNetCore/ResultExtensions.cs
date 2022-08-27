using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnnotatedResult.AspNetCore;

public static class ResultExtensions
{
    public static IResult HttpResult(this Result result)
    {
        if(result.IsSuccess)
        {
            return Results.Ok();
        }

        var errors = new List<string>();
        foreach(var error in result.Errors)
        {
            errors.Add(error.Message);
        }
        var details = new ProblemDetails
        {
            Status = (int)result.Status
        };
        details.Extensions["errors"] = errors;
        return Results.Problem(details);
    }
}
