using System.Collections.Generic;

namespace AnnotatedResult.Internal
{
    internal static class StringExtensions
    {
        internal static string Format(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        internal static string Join(this List<string> values, string separator)
        {
            return string.Join(separator, values);
        }
    }
}