using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AnnotatedResult.Internal
{
    internal sealed class InternalValidator : IResultValidator
    {
        private readonly List<ValidationResult> _results;
        private readonly List<Error> _errors;

        internal InternalValidator()
        {
            _results = new List<ValidationResult>();
            _errors = new List<Error>();
        }

        internal ValidationResult ValidationResult => _results[_results.Count - 1];

        public bool TryValidate<T>(T instance, out List<Error> errors)
        {
            var properties = instance.GetType().GetProperties();
            foreach(var property in properties)
            {
                ValidateRequired(instance, property);
                ValidateOptional(instance, property);
            }

            errors = _errors;
            return _results.Count == 0;
        }

        internal List<ValidationResult> Validate<T>(T instance, out List<Error> errors)
        {
            TryValidate(instance, out _);
            errors = _errors;
            return _results;
        }

        private void ValidateRequired<T>(T instance, PropertyInfo property)
        {
            if(property.IsRequired())
            {
                ValidateProperty(instance, property, ErrorSeverity.Error);
            }
        }

        private void ValidateOptional<T>(T instance, PropertyInfo property)
        {
            if(property.IsOptional())
            {
                ValidateProperty(instance, property, ErrorSeverity.Warning);
            }
        }

        private void ValidateProperty<T>(T instance, PropertyInfo property, ErrorSeverity severity)
        {
            var context = new ValidationContext(instance, null, null)
            {
                MemberName = property.Name
            };
            var value = instance.GetType().GetProperty(property.Name)?.GetValue(instance);
            var isValid = Validator.TryValidateProperty(value, context, _results);
            if(isValid)
            {
                return;
            }

            if(!property.HasCompositeValidation())
            {
                _errors.Add(new Error(ValidationResult.ErrorMessage, severity));
                return;
            }

            var errorStrings = ValidationResult.ErrorMessage.Split('|');
            foreach (var error in errorStrings)
            {
                var errorString = error.Split('`');
                var errorSeverity = (ErrorSeverity)Enum.Parse(typeof(ErrorSeverity), errorString[0]);
                var errorMessage = errorString[1];
                _errors.Add(new Error(errorMessage, errorSeverity));
            }
        }
    }
}