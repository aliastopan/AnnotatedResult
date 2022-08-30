namespace AnnotatedResult
{
    public struct Error
    {
        public string Message { get; }
        public ErrorSeverity Severity { get; set; }

        public Error(string message, ErrorSeverity severity = ErrorSeverity.Error)
        {
            Message = message;
            Severity = severity;
        }
    }
}