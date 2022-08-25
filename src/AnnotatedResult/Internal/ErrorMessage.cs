namespace AnnotatedResult.Internal
{
    internal static class ErrorMessage
    {
        internal const string Header = "Validation for {0} failed.";
        internal const string Unauthorized = "Your request has been denied.";
        internal const string Forbidden = "You don't have permission to access this resource.";
        internal const string Conflict = "Your request cannot be processed.";
        internal const string NotFound = "Resource not found.";
    }
}