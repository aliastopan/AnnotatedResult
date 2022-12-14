using System;
using AnnotatedResult.Internal;

namespace AnnotatedResult
{
    public class Result<T> : Result
    {
        internal protected Result(T value, ResultStatus status)
            : base(status)
        {
            Value = value;
        }

        internal protected Result(T value, ResultStatus status, params Error[] errors)
            : base(status, errors)
        {
            Value = value;
        }

        public static implicit operator T(Result<T> result) => result.Value;

        public T Value { get; private set; }
        public Type ValueType => Value.GetType();

        public static Result<T> Validate(T value, IResultValidator validator)
        {
            var isValid = validator.TryValidate(value, out Error[] errors);
            if(isValid)
            {
                return Result<T>.Ok(value);
            }

            return Result<T>.Invalid(errors);
        }

        public static Result<T> Validate(T value)
        {
            return Result<T>.Validate(value, new InternalValidator());
        }

        public static Result<T> Ok(T value)
        {
            return new Result<T>(value, ResultStatus.Ok);
        }

        public static new Result<T> Error(int errorCode, params Error[] errors)
        {
            return new Result<T>(default, (ResultStatus)errorCode, errors);
        }

        public static new Result<T> Error(params Error[] errors)
        {
            return new Result<T>(default, ResultStatus.Error, errors);
        }

        public static new Result<T> Invalid(params Error[] errors)
        {
            return new Result<T>(default, ResultStatus.Invalid, errors);
        }

        public static new Result<T> Unauthorized(params Error[] errors)
        {
            return new Result<T>(default, ResultStatus.Unauthorized, ErrorResult.Unauthorized(errors));
        }

        public static new Result<T> Forbidden(params Error[] errors)
        {
            return new Result<T>(default, ResultStatus.Forbidden, ErrorResult.Forbidden(errors));
        }

        public static new Result<T> Conflict(params Error[] errors)
        {
            return new Result<T>(default, ResultStatus.Conflict, ErrorResult.Conflict(errors));
        }

        public static new Result<T> NotFound(params Error[] errors)
        {
            return new Result<T>(default, ResultStatus.NotFound, ErrorResult.NotFound(errors));
        }
    }
}