using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnnotatedResult.Common
{
    public class ResultValidator
    {
        public bool TryValidate<T>(T instance, out List<ValidationResult> results)
        {
            results = new List<ValidationResult>();
            var context = new ValidationContext(instance);
            Validator.TryValidateObject(instance, context, results, true);

            return results.Count == 0;
        }
    }
}