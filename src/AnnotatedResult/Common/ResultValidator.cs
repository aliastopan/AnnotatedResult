using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnnotatedResult.Common
{
    public static class ResultValidator
    {
        public static bool TryValidate<T>(T instance, out List<ValidationResult> results)
        {
            results = new List<ValidationResult>();
            var context = new ValidationContext(instance);
            Validator.TryValidateObject(instance, context, results, true);

            return results.Count == 0;
        }
    }
}