using System.Collections.Generic;

namespace AnnotatedResult;

/// <summary>
/// Extension class for ProblemDetails providing additional error details.
/// </summary>
public class ProblemDetailsExtension
{
    /// <summary>
    /// List of errors.
    /// </summary>
    public List<Error> Errors { get; set; } = new List<Error>();

    /// <summary>
    /// Adds a new error with the specified message and severity.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="errorSeverity">The severity of the error.</param>
    public void AddError(string message, ErrorSeverity errorSeverity)
    {
        Errors.Add(new Error(message, errorSeverity));
    }
}
