using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AnnotatedResult.Internal;

namespace AnnotatedResult.DataAnnotations
{
    /// <summary>
    /// Attribute for validating complex objects using data annotations.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ComplexPropertyAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the specified value and returns a <see cref="ValidationResult"/> indicating whether validation succeeded.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <param name="context">The context information about the validation operation.</param>
        /// <returns>
        /// A <see cref="ValidationResult"/> instance that indicates the result of the validation.
        /// Returns <see cref="ValidationResult.Success"/> if validation passes; otherwise, returns a result containing validation errors.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext context)
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
            var result = new ComplexPropertyValidationResult(errorMessage);
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