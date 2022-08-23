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
    }
}