namespace AnnotatedResult.Internal
{
    internal static class ErrorResult
    {
        internal static Error[] Unauthorized(params Error[] errors)
        {
            return Error(ErrorMessage.Unauthorized, errors);
        }

        internal static Error[] Forbidden(params Error[] errors)
        {
            return Error(ErrorMessage.Forbidden, errors);
        }

        internal static Error[] Conflict(params Error[] errors)
        {
            return Error(ErrorMessage.Conflict, errors);
        }

        internal static Error[] NotFound(params Error[] errors)
        {
            return Error(ErrorMessage.NotFound, errors);
        }

        private static Error[] Error(string errorMessage, params Error[] errors)
        {
            return errors.Length != 0
                ? errors
                : new Error[]
                {
                    new Error(errorMessage, ErrorSeverity.Error)
                };
        }
    }
}