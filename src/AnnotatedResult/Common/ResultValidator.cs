using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnnotatedResult.Common
{
    public class ResultValidator : IResultValidator
    {
        public bool TryValidate<T>(T instance, out List<string> errorMessages)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(instance);
            Validator.TryValidateObject(instance, context, results, true);

            errorMessages = new List<string>();
            foreach(var result in results)
            {
                errorMessages.Add(result.ErrorMessage);
            }

            return results.Count == 0;
        }
    }
}