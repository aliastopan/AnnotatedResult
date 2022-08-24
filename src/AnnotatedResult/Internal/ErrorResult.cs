namespace AnnotatedResult.Internal
{
    internal static class ErrorResult
    {
        internal static Error Unauthorized => new Error(ErrorMessage.Unauthorized, ErrorSeverity.Error);
        internal static Error Forbidden => new Error(ErrorMessage.Forbidden, ErrorSeverity.Error);
        internal static Error Conflict => new Error(ErrorMessage.Conflict, ErrorSeverity.Error);
        internal static Error NotFound => new Error(ErrorMessage.NotFound, ErrorSeverity.Error);
    }
}