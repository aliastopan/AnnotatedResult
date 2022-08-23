using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnnotatedResult.Common
{
    public interface IResultValidator
    {
        bool TryValidate<T>(T instance, out List<ValidationResult> results);
    }
}