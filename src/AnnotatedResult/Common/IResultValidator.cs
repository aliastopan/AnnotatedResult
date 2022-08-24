using System.Collections.Generic;

namespace AnnotatedResult.Common
{
    public interface IResultValidator
    {
        bool TryValidate<T>(T instance, out List<Error> errors);
    }
}