using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using AnnotatedResult.DataAnnotations;

namespace AnnotatedResult.Internal
{
    internal static class PropertyInfoExtensions
    {
        internal static bool IsRequired(this PropertyInfo property)
        {
            return Attribute.IsDefined(property, typeof(RequiredAttribute));
        }

        internal static bool IsOptional(this PropertyInfo property)
        {
            return Attribute.IsDefined(property, typeof(ValidationAttribute)) && !property.IsRequired();
        }

        internal static bool HasValidateObjectAttribute(this PropertyInfo property)
        {
            return Attribute.IsDefined(property, typeof(ValidateObjectAttribute));
        }

        internal static bool HasComplexPropertyAttribute(this PropertyInfo property)
        {
            return Attribute.IsDefined(property, typeof(ComplexPropertyAttribute));
        }
    }
}