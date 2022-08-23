using System.Collections.Generic;
using System.Collections.ObjectModel;
using AnnotatedResult.Common;

namespace AnnotatedResult
{
    public class Result
    {
        private readonly List<string> _errorMessages;

        internal Result(ResultStatus status)
        {
            Status = status;
        }

        internal Result(ResultStatus status, params string[] errorMessages)
        {
            Status = status;
            _errorMessages = new List<string>(errorMessages);
        }

        public ResultStatus Status { get; protected set; }
        public bool IsSuccess => Status == ResultStatus.Ok;
        public ReadOnlyCollection<string> Errors => _errorMessages.AsReadOnly();

        public static Result Ok()
        {
            return new Result(ResultStatus.Ok);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, ResultStatus.Ok);
        }

        public static Result Error(params string[] errorMessages)
        {
            return new Result(ResultStatus.Error, errorMessages);
        }

        public static Result<T> Error<T>(params string[] errorMessages)
        {
            return new Result<T>(default, ResultStatus.Error, errorMessages);
        }

        public static Result<T> Invalid<T>(params string[] errorMessages)
        {
            return new Result<T>(default, ResultStatus.Invalid, errorMessages);
        }

        public static Result<T> Validate<T>(T value)
        {
            bool isValid = ResultValidator.TryValidate(value, out var results);

            if(isValid)
                return Result.Ok(value);

            var errors = new List<string>();
            results.ForEach(error => errors.Add(error.ErrorMessage));
            return Result.Invalid<T>(errors.ToArray());
        }

        public static Result<T> Unauthorized<T>()
        {
            return new Result<T>(default, ResultStatus.Unauthorized);
        }

        public static Result<T> Forbidden<T>()
        {
            return new Result<T>(default, ResultStatus.Forbidden);
        }

        public static Result<T> Conflict<T>()
        {
            return new Result<T>(default, ResultStatus.Conflict);
        }

        public static Result<T> NotFound<T>()
        {
            return new Result<T>(default, ResultStatus.NotFound);
        }
    }
}