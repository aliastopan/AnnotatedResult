using System.Collections.Generic;
using System.Collections.ObjectModel;
using AnnotatedResult.Common;

namespace AnnotatedResult
{
    public class Result
    {
        private readonly List<string> _errors;

        internal Result(ResultStatus status)
        {
            Status = status;
        }

        internal Result(ResultStatus status, IEnumerable<string> errors)
        {
            Status = status;
            _errors = new List<string>(errors);
        }

        public ResultStatus Status { get; protected set; }
        public bool IsSuccess => Status == ResultStatus.Ok;
        public ReadOnlyCollection<string> Errors => _errors.AsReadOnly();

        public static Result Ok()
        {
            return new Result(ResultStatus.Ok);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, ResultStatus.Ok);
        }

        public static Result Error(IEnumerable<string> errors)
        {
            return new Result(ResultStatus.Error, errors);
        }

        public static Result Error<T>(IEnumerable<string> errors)
        {
            return new Result<T>(default, ResultStatus.Error, errors);
        }

        public static Result<T> Invalid<T>(IEnumerable<string> errors)
        {
            return new Result<T>(default, ResultStatus.Invalid, errors);
        }

        public static Result<T> Validate<T>(T value)
        {
            bool isValid = ResultValidator.TryValidate(value, out var results);

            if(isValid)
                return Result.Ok(value);

            var errors = new List<string>();
            results.ForEach(error => errors.Add(error.ErrorMessage));
            return Result.Invalid<T>(errors);
        }
    }
}