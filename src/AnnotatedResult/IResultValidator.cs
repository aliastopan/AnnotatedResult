namespace AnnotatedResult
{
    public interface IResultValidator
    {
        bool TryValidate<T>(T instance, out Error[] errors);
    }
}