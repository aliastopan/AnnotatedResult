using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AnnotatedResult.Internal;

namespace AnnotatedResult.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ValidateObjectAttribute : ValidationAttribute
    {
        public bool ErrorHeader { get; set; } = true;

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var invalids = TryValidate(value, out var errors);
            if(invalids.Count == 0)
            {
                return ValidationResult.Success;
            }

            var errorStrings = ErrorStrings(value);
            foreach(var error in errors)
            {
                errorStrings.Add(string.Format("{0}`{1}", error.Severity, error.Message));
            }

            var result = new CompositeValidationResult(
                string.Join("|", errorStrings),
                invalids[0].MemberNames);

            invalids.ForEach(invalid => result.Add(invalid));
            return result;
        }

        private static List<ValidationResult> TryValidate(object value, out List<Error> errors)
        {
            var validator = new InternalValidator();
            var results = validator.Validate(value, out var errorList);
            errors = errorList;
            return results;
        }

        private List<string> ErrorStrings(object value)
        {
            if(!ErrorHeader)
            {
                return new List<string>();
            }

            return new List<string>
            {
                Header(value)
            };
        }

        private string Header(object value)
        {
            var property = GetParentProperty(value);
            var errorMessage = this.ErrorMessage ?? string.Format("Validation for {0} failed.", property);
            var error = string.Format("{0}`{1}", ErrorSeverity.Error, errorMessage);
            return error;
        }

        private string GetParentProperty(object value)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(value, null, null);
            Validator.TryValidateObject(value, context, results, true);
            return context.DisplayName;
        }
    }
}