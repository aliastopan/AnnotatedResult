using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AnnotatedResult.Internal;

namespace AnnotatedResult.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ValidateObjectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var invalids = TryValidate(value, out var errors);
            if (invalids.Count == 0)
            {
                return ValidationResult.Success;
            }

            return ResultError(invalids, errors);
        }

        private ValidationResult ResultError(List<ValidationResult> invalids, List<Error> errors)
        {
            var errorStrings = ErrorStrings();
            foreach (var error in errors)
            {
                errorStrings.Add("{0}`{1}".Format(error.Severity, error.Message.Sanitize()));
            }

            var errorMessage = errorStrings.Join(separator: "|");
            var result = new CompositeValidationResult(errorMessage);
            invalids.ForEach(invalid => result.Add(invalid));
            return result;
        }

        private static List<ValidationResult> TryValidate(object value, out List<Error> errors)
        {
            var validator = new InternalValidator();
            var results = validator.Validate(value, out errors);
            return results;
        }

        private List<string> ErrorStrings()
        {
            return this.ErrorMessage.IsBlank()
                ? new List<string>()
                : new List<string>
                {
                    "{0}`{1}".Format(ErrorSeverity.Error, this.ErrorMessage)
                };
        }
    }
}