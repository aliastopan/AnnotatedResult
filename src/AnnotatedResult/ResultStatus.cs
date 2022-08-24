namespace AnnotatedResult
{
    public enum ResultStatus
    {
        Ok = 200,
        Error = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
        Invalid = 422,
    }
}