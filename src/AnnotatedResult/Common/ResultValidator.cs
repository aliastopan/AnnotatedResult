using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnnotatedResult.Common
{
    public class ResultValidator : IResultValidator
    {
        public bool TryValidate<T>(T instance, out List<Error> errors)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(instance);
            Validator.TryValidateObject(instance, context, results, true);

            errors = new List<Error>();
            foreach(var result in results)
            {
                var error = new Error(result.ErrorMessage);
                errors.Add(error);
            }

            return results.Count == 0;
        }
    }
}