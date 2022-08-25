using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AnnotatedResult.Internal;

namespace AnnotatedResult.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidateObjectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var results = Validate(value, out var displayName);
            if(results.Count == 0)
            {
                return ValidationResult.Success;
            }

            var validationResult = new CompositeValidationResult(
                string.Format("Validation for {0} failed.",
                displayName));

            results.ForEach(validationResult.Add);
            return validationResult;
        }

        private static List<ValidationResult> Validate(object value, out string displayName)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(value, null, null);
            Validator.TryValidateObject(value, context, results, true);
            displayName = context.DisplayName;
            return results;
        }
    }
}