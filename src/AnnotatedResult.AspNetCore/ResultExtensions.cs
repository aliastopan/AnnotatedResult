using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnnotatedResult;

/// <summary>
/// Provides extension methods for working with results in ASP.NET Core applications.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Retrieves the extension for the <see cref="ProblemDetails"/> object.
    /// </summary>
    /// <param name="problemDetails">The <see cref="ProblemDetails"/> object.</param>
    /// <returns>The extension for the <see cref="ProblemDetails"/>.</returns>
    public static ProblemDetailsExtension GetExtension(this ProblemDetails problemDetails)
    {
        var problemDetailsExtension = new ProblemDetailsExtension();

        JsonElement jsonErrors = (JsonElement)problemDetails!.Extensions["errors"];
        JsonElement jsonArray = JsonSerializer.Deserialize<JsonElement>(jsonErrors);

        foreach (JsonElement jsonElement in jsonArray.EnumerateArray())
        {
            string message = jsonElement.GetProperty("message").GetString();
            string severity = jsonElement.GetProperty("severity").GetString();

            _ = Enum.TryParse<ErrorSeverity>(severity, out var errorSeverity);
            problemDetailsExtension.AddError(message, errorSeverity);
        }

        return problemDetailsExtension;
    }

    /// <summary>
    /// Converts a tuple containing a <see cref="ResultStatus"/> and a read-only collection of <see cref="Error"/> objects
    /// into an <see cref="IResult"/> representing a problem response, using the provided <see cref="ProblemDetails"/>.
    /// </summary>
    /// <param name="error">
    /// A tuple containing the result status and a read-only collection of errors to be included in the problem response.
    /// </param>
    /// <param name="details">
    /// The <see cref="ProblemDetails"/> object to populate with error information and status code.
    /// </param>
    /// <returns>
    /// An <see cref="IResult"/> representing the problem response, with error details included in the extensions.
    /// </returns>
    public static IResult WithProblemDetails(this (ResultStatus status, ReadOnlyCollection<Error> list) error,
        ProblemDetails details)
    {
        var errors = new List<object>();
        foreach (var err in error.list)
        {
            errors.Add(new
            {
                message = err.Message,
                severity = err.Severity.ToString()
            });
        }
        details.Status = (int)error.status;
        details.Extensions["errors"] = errors;
        return Results.Problem(details);
    }

    /// <summary>
    /// Converts a tuple containing a <see cref="ResultStatus"/> and a read-only collection of <see cref="Error"/> objects
    /// into an <see cref="IResult"/> formatted as a problem details response.
    /// </summary>
    /// <param name="error">
    /// A tuple containing the result status and a read-only collection of errors to be included in the problem details.
    /// </param>
    /// <param name="details">
    /// The <see cref="ProblemDetails"/> object to populate with error information.
    /// </param>
    /// <param name="context">
    /// The current <see cref="HttpContext"/>, used to set the instance and trace identifier in the problem details.
    /// </param>
    /// <returns>
    /// An <see cref="IResult"/> representing the problem details response, including error messages, severities, and trace information.
    /// </returns>
    public static IResult WithProblemDetails(this (ResultStatus status, ReadOnlyCollection<Error> list) error,
        ProblemDetails details,
        HttpContext context)
    {
        var errors = new List<object>();
        foreach (var err in error.list)
        {
            errors.Add(new
            {
                message = err.Message,
                severity = err.Severity.ToString()
            });
        }
        details.Status = (int)error.status;
        details.Instance = context.Request.Path;
        details.Extensions["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier;
        details.Extensions["errors"] = errors;
        return Results.Problem(details);
    }

    /// <summary>
    /// Creates an <see cref="IResult"/> containing problem details based on the provided error information and <see cref="ProblemDetails"/> instance.
    /// </summary>
    /// <param name="context">The current <see cref="HttpContext"/>.</param>
    /// <param name="error">
    /// A tuple containing the <see cref="ResultStatus"/> and a read-only collection of <see cref="Error"/> objects representing the errors to include in the response.
    /// </param>
    /// <param name="details">
    /// The <see cref="ProblemDetails"/> instance to populate with error information and additional metadata.
    /// </param>
    /// <returns>
    /// An <see cref="IResult"/> representing the problem details response, including error messages, severity, trace identifier, and request path.
    /// </returns>
    public static IResult WithProblemDetails(this HttpContext context,
        (ResultStatus status, ReadOnlyCollection<Error> list) error,
        ProblemDetails details)
    {
        var errors = new List<object>();
        foreach (var err in error.list)
        {
            errors.Add(new
            {
                message = err.Message,
                severity = err.Severity.ToString()
            });
        }
        details.Status = (int)error.status;
        details.Instance = context.Request.Path;
        details.Extensions["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier;
        details.Extensions["errors"] = errors;
        return Results.Problem(details);
    }

    /// <summary>
    /// Converts a <see cref="Result"/> to an <see cref="IResult"/> for ASP.NET Core endpoints.
    /// </summary>
    /// <param name="result">The result to convert.</param>
    /// <returns>An <see cref="IResult"/> representing the outcome.</returns>
    public static IResult AsIResult(this Result result)
    {
        return Match(result);
    }

    /// <summary>
    /// Converts a <see cref="Result{T}"/> to an <see cref="IResult"/> for ASP.NET Core endpoints.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result.</typeparam>
    /// <param name="result">The result to convert.</param>
    /// <returns>An <see cref="IResult"/> representing the outcome.</returns>
    public static IResult AsIResult<T>(this Result<T> result)
    {
        return Match(result);
    }

    /// <summary>
    /// Converts a <see cref="Result"/> to an <see cref="IResult"/>, using the specified <see cref="ProblemDetails"/> for errors.
    /// </summary>
    /// <param name="result">The result to convert.</param>
    /// <param name="details">The <see cref="ProblemDetails"/> to use if the result is not successful.</param>
    /// <returns>An <see cref="IResult"/> representing the outcome.</returns>
    public static IResult AsIResult(this Result result, ProblemDetails details)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Results.Ok(),
            _ => result.Problem(details)
        };
    }

    /// <summary>
    /// Converts a <see cref="Result"/> to an <see cref="IResult"/>, using the specified <see cref="HttpContext"/> for error details.
    /// </summary>
    /// <param name="result">The result to convert.</param>
    /// <param name="context">The current <see cref="HttpContext"/>.</param>
    /// <returns>An <see cref="IResult"/> representing the outcome.</returns>
    public static IResult AsIResult(this Result result, HttpContext context)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Results.Ok(),
            _ => result.Problem(context)
        };
    }

    /// <summary>
    /// Converts a <see cref="Result"/> to an <see cref="IResult"/>, using the specified <see cref="HttpContext"/> and <see cref="ProblemDetails"/> for errors.
    /// </summary>
    /// <param name="result">The result to convert.</param>
    /// <param name="context">The current <see cref="HttpContext"/>.</param>
    /// <param name="details">The <see cref="ProblemDetails"/> to use if the result is not successful.</param>
    /// <returns>An <see cref="IResult"/> representing the outcome.</returns>
    public static IResult AsIResult(this Result result, HttpContext context, ProblemDetails details)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Results.Ok(),
            _ => result.Problem(context, details)
        };
    }

    /// <summary>
    /// Converts a <see cref="Result{T}"/> to an <see cref="IResult"/>, using the specified <see cref="HttpContext"/> for error details.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result.</typeparam>
    /// <param name="result">The result to convert.</param>
    /// <param name="context">The current <see cref="HttpContext"/>.</param>
    /// <returns>An <see cref="IResult"/> representing the outcome.</returns>
    public static IResult AsIResult<T>(this Result<T> result, HttpContext context)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Results.Ok(result.Value),
            _ => result.Problem(context)
        };
    }

    /// <summary>
    /// Converts a <see cref="Result{T}"/> to an <see cref="IResult"/>, using the specified <see cref="ProblemDetails"/> for errors.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result.</typeparam>
    /// <param name="result">The result to convert.</param>
    /// <param name="details">The <see cref="ProblemDetails"/> to use if the result is not successful.</param>
    /// <returns>An <see cref="IResult"/> representing the outcome.</returns>
    public static IResult AsIResult<T>(this Result<T> result, ProblemDetails details)
    {
        return result.Status switch
        {
            ResultStatus.Ok => Results.Ok(result.Value),
            _ => result.Problem(details)
        };
    }

    /// <summary>
    /// Converts a <see cref="Result{T}"/> to an <see cref="IResult"/>, using the specified <see cref="HttpContext"/> and <see cref="ProblemDetails"/> for errors.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result.</typeparam>
    /// <param name="result">The result to convert.</param>
    /// <param name="context">The current <see cref="HttpContext"/>.</param>
    /// <param name="details">The <see cref="ProblemDetails"/> to use if the result is not successful.</param>
    /// <returns>An <see cref="IResult"/> representing the outcome.</returns>
    public static IResult AsIResult<T>(this Result<T> result, HttpContext context, ProblemDetails details)
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
