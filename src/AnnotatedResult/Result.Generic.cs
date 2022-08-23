using System.Collections.Generic;
using AnnotatedResult.Common;

namespace AnnotatedResult
{
    public class Result<T> : Result
    {
        internal protected Result(T value, ResultStatus status)
            : base(status)
        {
            Value = value;
        }

        internal protected Result(T value, ResultStatus status, IEnumerable<string> errors)
            : base(status, errors)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }
}