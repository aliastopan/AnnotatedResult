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

            var result = new CompositeValidationResult(
                errorStrings.Join(separator: "|"));

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
            if(this.ErrorMessage.IsBlank())
            {
                return new List<string>();
            }

            return new List<string>
            {
                ErrorHeader()
            };
        }

        private string ErrorHeader()
        {
            var errorMessage = this.ErrorMessage;
            var error = "{0}`{1}".Format(ErrorSeverity.Error, errorMessage);
            return error;
        }
    }
}