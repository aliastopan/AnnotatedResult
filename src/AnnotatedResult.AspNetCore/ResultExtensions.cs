using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnnotatedResult.AspNetCore;

public static class ResultExtensions
{
    public static IResult HttpResult(this Result result)
    {
        return Match(result);
    }

    public static IResult HttpResult<T>(this Result<T> result)
    {
        return Match(result);
    }

    private static IResult Match(Result result)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Results.Ok(),
            ResultStatus.Conflict => Results.Conflict(),
            ResultStatus.Unauthorized => Results.Unauthorized(),
            ResultStatus.Forbidden => Results.Forbid(),
            ResultStatus.Invalid => Results.UnprocessableEntity(),
            _ => Results.Problem(new ProblemDetails
            {
                Status = (int)result.Status
            })
        };
    }
}
