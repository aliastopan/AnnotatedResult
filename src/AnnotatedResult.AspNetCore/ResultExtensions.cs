using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnnotatedResult;

public static class ResultExtensions
{
    public static IResult AsProblem(this (ResultStatus status, ReadOnlyCollection<Error> list) error, ProblemDetails details)
    {
        var errors = new List<string>();
        foreach(var err in error.list)
        {
            errors.Add(err.Message);
        }
        details.Status = (int)error.status;
        details.Extensions["errors"] = errors;
        return Results.Problem(details);
    }

    public static IResult AsProblem(this (ResultStatus status, ReadOnlyCollection<Error> list) error, ProblemDetails details, HttpContext context)
    {
        var errors = new List<string>();
        foreach(var err in error.list)
        {
            errors.Add(err.Message);
        }
        details.Status = (int)error.status;
        details.Extensions["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier;
        details.Extensions["errors"] = errors;
        return Results.Problem(details);
    }

    public static IResult HttpResult(this Result result)
    {
        return Match(result);
    }

    public static IResult HttpResult<T>(this Result<T> result)
    {
        return Match(result);
    }

    public static IResult HttpResult(this Result result, ProblemDetails details)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Results.Ok(),
            _ => result.Problem(details)
        };
    }

    public static IResult HttpResult(this Result result, HttpContext context)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Results.Ok(),
            _ => result.Problem(context)
        };
    }

    public static IResult HttpResult(this Result result, HttpContext context, ProblemDetails details)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Results.Ok(),
            _ => result.Problem(context, details)
        };
    }

    public static IResult HttpResult<T>(this Result<T> result, HttpContext context)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Results.Ok(result.Value),
            _ => result.Problem(context)
        };
    }

    public static IResult HttpResult<T>(this Result<T> result, ProblemDetails details)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Results.Ok(result.Value),
            _ => result.Problem(details)
        };
    }

    public static IResult HttpResult<T>(this Result<T> result, HttpContext context, ProblemDetails details)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Results.Ok(result.Value),
            _ => result.Problem(context, details)
        };
    }

    private static IResult Problem(this Result result, HttpContext context)
    {
        var errors = new List<string>();
        foreach(var error in result.Errors)
        {
            errors.Add(error.Message);
        }

        var details = new ProblemDetails
        {
            Status = (int)result.Status,
            Instance = context.Request.Path
        };
        details.Extensions["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier;
        return Results.Problem(details);
    }

    private static IResult Problem(this Result result, ProblemDetails details)
    {
        var errors = new List<string>();
        foreach (var error in result.Errors)
        {
            errors.Add(error.Message);
        }
        details.Status = (int)result.Status;
        details.Extensions["errors"] = errors;
        return Results.Problem(details);
    }

    private static IResult Problem(this Result result, HttpContext context, ProblemDetails details)
    {
        var errors = new List<string>();
        foreach (var error in result.Errors)
        {
            errors.Add(error.Message);
        }
        details.Status = (int)result.Status;
        details.Instance = context.Request.Path;
        details.Extensions["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier;
        details.Extensions["errors"] = errors;
        return Results.Problem(details);
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
