namespace AnnotatedResult.Common
{
    public struct Error
    {
        public string Message { get; }
        public ErrorSeverity Severity { get; set; }

        public Error(string message, ErrorSeverity severity)
        {
            Message = message;
            Severity = severity;
        }
    }
}