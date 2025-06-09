using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AnnotatedResult.Internal;

namespace AnnotatedResult.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ValidateObjectAttribute : ValidationAttribute
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected override ValidationResult IsValid(object value, ValidationContext context)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            var invalids = TryValidate(value, out List<Error> errors);
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