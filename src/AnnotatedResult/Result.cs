using System.Collections.Generic;
using System.Collections.ObjectModel;
using AnnotatedResult.Common;

namespace AnnotatedResult
{
    public class Result
    {
        private readonly List<Error> _errors;

        internal Result(ResultStatus status)
        {
            Status = status;
            _errors = new List<Error>();
        }

        internal Result(ResultStatus status, params Error[] errors)
        {
            Status = status;
            _errors = new List<Error>(errors);
        }

        public ResultStatus Status { get; protected set; }
        public bool IsSuccess => Status == ResultStatus.Ok;
        public ReadOnlyCollection<Error> Errors => _errors.AsReadOnly();

        public static Result Ok()
        {
            return new Result(ResultStatus.Ok);
        }

        public static Result Error(params Error[] errors)
        {
            return new Result(ResultStatus.Error, errors);
        }

        public static Result Invalid(params Error[] errors)
        {
            return new Result(ResultStatus.Invalid, errors);
        }

        public static Result Unauthorized()
        {
            return new Result(ResultStatus.Unauthorized);
        }

        public static Result Forbidden()
        {
            return new Result(ResultStatus.Forbidden);
        }

        public static Result Conflict()
        {
            return new Result(ResultStatus.Conflict);
        }

        public static Result NotFound()
        {
            return new Result(ResultStatus.NotFound);
        }
    }
}