namespace AnnotatedResult
{
    /// <summary>
    /// Represents an error with a specific message and severity level.
    /// </summary>
    public struct Error
    {
        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets or sets the severity level of the error.
        /// </summary>
        public ErrorSeverity Severity { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> struct with the specified message and optional severity.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="severity">The severity level of the error (default is <see cref="ErrorSeverity.Warning"/>).</param>
        public Error(string message, ErrorSeverity severity = ErrorSeverity.Warning)
        {
            Message = message;
            Severity = severity;
        }
    }
}