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
            var results = TryValidate(value, out var errorResults);
            if(results.Count == 0)
            {
                return ValidationResult.Success;
            }

            var errors = new List<string>
            {
                ErrorHeader(value)
            };
            foreach(var error in errorResults)
            {
                errors.Add(string.Format("{0}`{1}", error.Severity, error.Message));
            }

            var validationResult = new CompositeValidationResult(
                string.Join("|", errors),
                results[0].MemberNames);

            results.ForEach(result => validationResult.Add(result));
            return validationResult;
        }

        private static List<ValidationResult> TryValidate(object value, out List<Error> errors)
        {
            var validator = new InternalValidator();
            var results = validator.Validate(value, out var errorList);
            errors = errorList;
            return results;
        }

        private static string ErrorHeader(object value)
        {
            var property = GetParentProperty(value);
            var severity = "Error";
            var error = string.Format("{0}`Validation for {1} failed.", severity, property);
            return error;
        }

        private static string GetParentProperty(object value)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(value, null, null);
            Validator.TryValidateObject(value, context, results, true);
            return context.DisplayName;
        }
    }
}