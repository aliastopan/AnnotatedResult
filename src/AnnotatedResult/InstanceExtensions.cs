using AnnotatedResult.Internal;

namespace AnnotatedResult
{
    /// <summary>
    /// Provides extension methods for validating instances.
    /// </summary>
    public static class InstanceExtensions
    {
        /// <summary>
        /// Attempts to validate the specified instance.
        /// </summary>
        /// <typeparam name="T">The type of the instance to validate.</typeparam>
        /// <param name="instance">The instance to validate.</param>
        /// <param name="errors">When this method returns, contains any errors that occurred during validation.</param>
        /// <returns><c>true</c> if the instance is valid; otherwise, <c>false</c>.</returns>
        public static bool TryValidate<T>(this T instance, out Error[] errors)
        {
            var validator = new InternalValidator();
            return validator.TryValidate(instance, out errors);
        }

        /// <summary>
        /// Attempts to validate the specified instance using the provided validator.
        /// </summary>
        /// <typeparam name="T">The type of the instance to validate.</typeparam>
        /// <param name="instance">The instance to validate.</param>
        /// <param name="validator">The validator to use.</param>
        /// <param name="errors">When this method returns, contains any errors that occurred during validation.</param>
        /// <returns><c>true</c> if the instance is valid; otherwise, <c>false</c>.</returns>
        public static bool TryValidate<T>(this T instance, IResultValidator validator, out Error[] errors)
        {
            return validator.TryValidate(instance, out errors);
        }
    }
}