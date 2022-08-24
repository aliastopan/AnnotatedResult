using System.Reflection;
using System.ComponentModel.DataAnnotations;
using AnnotatedResult.SampleTests.Models;

namespace AnnotatedResult.SampleTests.Samples;

public static class CustomValidation
{
    internal static bool IsRequired(PropertyInfo property)
    {
        return Attribute.IsDefined(property, typeof(RequiredAttribute));
    }

    internal static bool IsOptional(PropertyInfo property)
    {
        return Attribute.IsDefined(property, typeof(ValidationAttribute)) && !IsRequired(property);
    }

    public static void Run()
    {
        var instance = new Dto
        {
            Username = "John Wick",
            Password = "FortisFortunaAdiuvat"
        };

        var results = new List<ValidationResult>();
        var props = instance.GetType().GetProperties();
        foreach (var prop in props)
        {
            if(!IsOptional(prop))
            {
                continue;
            }

            var context = new ValidationContext(instance, null, null)
            {
                MemberName = prop.Name
            };
            Serilog.Log.Information("Property: {0}", prop.Name);
            var property = instance.GetType().GetProperty(prop.Name)?.GetValue(instance);
            Validator.TryValidateProperty(property, context, results);
        }

        foreach(var result in results)
        {
            Serilog.Log.Information(result?.ErrorMessage!);
        }
    }
}
