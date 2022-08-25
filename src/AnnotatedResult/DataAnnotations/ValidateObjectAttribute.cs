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

            var errors = new List<string>();
            foreach(var err in errorResults)
            {
                errors.Add(string.Format("{0}`{1}", err.Severity, err.Message));
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
    }
}