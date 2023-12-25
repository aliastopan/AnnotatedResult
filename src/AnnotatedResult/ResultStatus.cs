namespace AnnotatedResult
{
    /// <summary>
    /// Represents the possible result statuses of an operation.
    /// </summary>
    public enum ResultStatus
    {
        /// <summary>
        /// Indicates a successful operation.
        /// </summary>
        Ok = 200,

        /// <summary>
        /// Indicates a general error in the operation.
        /// </summary>
        Error = 400,

        /// <summary>
        /// Indicates that the operation is unauthorized.
        /// </summary>
        Unauthorized = 401,

        /// <summary>
        /// Indicates that the operation is forbidden.
        /// </summary>
        Forbidden = 403,

        /// <summary>
        /// Indicates that the requested resource was not found.
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// Indicates a conflict in the operation.
        /// </summary>
        Conflict = 409,

        /// <summary>
        /// Indicates that the operation is invalid or cannot be processed.
        /// </summary>
        Invalid = 422,
    }
}