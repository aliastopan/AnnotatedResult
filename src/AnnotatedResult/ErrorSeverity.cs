namespace AnnotatedResult
{
    /// <summary>
    /// Represents the severity levels of errors.
    /// </summary>
    public enum ErrorSeverity
    {
        /// <summary>
        /// Informational messages that do not indicate an issue.
        /// </summary>
        Information,

        /// <summary>
        /// Messages used during debugging and development to provide detailed information for troubleshooting.
        /// </summary>
        Debug,

        /// <summary>
        /// Events that are noteworthy but may not require immediate attention.
        /// </summary>
        Notice,

        /// <summary>
        /// Indicates a potential problem or anomaly that doesn't necessarily prevent the system from functioning.
        /// </summary>
        Warning,

        /// <summary>
        /// Represents a critical issue that prevents the normal functioning of the system or application.
        /// </summary>
        Error,

        /// <summary>
        /// Similar to "Error," indicating a severe problem that requires immediate attention.
        /// </summary>
        Critical,

        /// <summary>
        /// Reserved for the most severe issues that require immediate attention and may result in a system failure.
        /// </summary>
        Emergency
    }
}