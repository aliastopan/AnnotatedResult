using AnnotatedResult.Internal;

namespace AnnotatedResult
{
    public static class InstanceExtensions
    {
        private readonly static InternalValidator validator = new InternalValidator();

        public static bool TryValidate<T>(this T instance, out Error[] errors)
        {
            return validator.TryValidate(instance, out errors);
        }
    }
}