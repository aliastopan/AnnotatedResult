using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AnnotatedResult.Common
{
    public class ResultValidator : IResultValidator
    {
        private readonly List<ValidationResult> _results;
        private readonly List<Error> _errors;

        public ResultValidator()
        {
            _results = new List<ValidationResult>();
            _errors = new List<Error>();
        }

        public bool TryValidate<T>(T instance, out List<Error> errors)
        {
            errors = _errors;
            var properties = instance.GetType().GetProperties();

            foreach(var property in properties)
            {
                ValidateProperty(IsRequired(property), instance, property);
                ValidateProperty(IsOptional(property), instance, property);
            }

            foreach(var validation in _results)
            {
                var error = new Error(validation.ErrorMessage);
                errors.Add(error);
            }

            return _results.Count == 0;
        }

        internal void ValidateProperty<T>(bool hasAttribute, T instance, PropertyInfo property)
        {
            if(!hasAttribute)
            {
                return;
            }

            var context = new ValidationContext(instance, null, null)
            {
                MemberName = property.Name
            };
            var value = instance.GetType().GetProperty(property.Name)?.GetValue(instance);
            Validator.TryValidateProperty(value, context, _results);
        }

        private static bool IsRequired(PropertyInfo property)
        {
            return Attribute.IsDefined(property, typeof(RequiredAttribute));
        }

        private static bool IsOptional(PropertyInfo property)
        {
            return Attribute.IsDefined(property, typeof(ValidationAttribute)) && !IsRequired(property);
        }
    }
}