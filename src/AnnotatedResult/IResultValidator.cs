namespace AnnotatedResult
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IResultValidator
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        bool TryValidate<T>(T instance, out Error[] errors);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}