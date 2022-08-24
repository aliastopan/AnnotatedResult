using System;
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

        internal protected Result(T value, ResultStatus status, params string[] errorMessages)
            : base(status, errorMessages)
        {
            Value = value;
        }

        public static implicit operator T(Result<T> result) => result.Value;

        public T Value { get; private set; }
        public Type ValueType => Value.GetType();

        public static Result<T> Validate(T value, IResultValidator validator)
        {
            var isValid = validator.TryValidate(value, out var errorMessages);
            if(isValid)
            {
                return Result<T>.Ok(value);
            }

            return Result<T>.Invalid(errorMessages.ToArray());
        }

        public static Result<T> Validate(T value)
        {
            return Result<T>.Validate(value, new ResultValidator());
        }

        public static Result<T> Ok(T value)
        {
            return new Result<T>(value, ResultStatus.Ok);
        }

        public static new Result<T> Error(params string[] errorMessages)
        {
            return new Result<T>(default, ResultStatus.Error, errorMessages);
        }

        public static new Result<T> Invalid(params string[] errorMessages)
        {
            return new Result<T>(default, ResultStatus.Invalid, errorMessages);
        }

        public static new Result<T> Unauthorized()
        {
            return new Result<T>(default, ResultStatus.Unauthorized);
        }

        public static new Result<T> Forbidden()
        {
            return new Result<T>(default, ResultStatus.Forbidden);
        }

        public static new Result<T> Conflict()
        {
            return new Result<T>(default, ResultStatus.Conflict);
        }

        public static new Result<T> NotFound()
        {
            return new Result<T>(default, ResultStatus.NotFound);
        }
    }
}