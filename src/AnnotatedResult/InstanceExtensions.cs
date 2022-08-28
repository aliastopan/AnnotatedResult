using AnnotatedResult.Internal;

namespace AnnotatedResult
{
    public static class InstanceExtensions
    {
        public static bool TryValidate<T>(this T instance, out Error[] errors)
        {
            var validator = new InternalValidator();
            return validator.TryValidate(instance, out errors);
        }

        public static bool TryValidate<T>(this T instance, IResultValidator validator, out Error[] errors)
        {
            return validator.TryValidate(instance, out errors);
        }
    }
}