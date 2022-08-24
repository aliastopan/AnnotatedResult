namespace AnnotatedResult.Common
{
    public struct Error
    {
        public string Message { get; }

        public Error(string message)
        {
            Message = message;
        }
    }
}