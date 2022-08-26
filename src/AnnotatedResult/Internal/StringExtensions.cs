using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AnnotatedResult.Internal
{
    internal static class StringExtensions
    {
        internal static bool IsBlank(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        internal static string Sanitize(this string input)
        {
            return Regex.Replace(input, "[`|]", "");
        }

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