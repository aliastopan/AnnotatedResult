using System;
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

        internal protected Result(T value, ResultStatus status, params string[] errorMessages)
            : base(status, errorMessages)
        {
            Value = value;
        }

        public T Value { get; private set; }
        public Type ValueType => Value.GetType();
    }
}