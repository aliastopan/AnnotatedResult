using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnnotatedResult.Internal
{
    internal sealed class CompositeValidationResult : ValidationResult
    {
        private readonly List<ValidationResult> _results;

        internal CompositeValidationResult(string errorMessage)
            : base(errorMessage)
        {
            _results = new List<ValidationResult>();
        }

        internal IEnumerable<ValidationResult> Results => _results;

        internal void Add(ValidationResult validationResult)
        {
            _results.Add(validationResult);
        }
    }
}