using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AnnotatedResult.Internal;

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

        public static Result Inherit(Result result)
        {
            return new Result(result.Status, result.Errors.ToArray());
        }

        public static Result Ok()
        {
            return new Result(ResultStatus.Ok);
        }

        public static Result Error(int errorCode, params Error[] errors)
        {
            return new Result((ResultStatus)errorCode, errors);
        }

        public static Result Error(params Error[] errors)
        {
            return new Result(ResultStatus.Error, errors);
        }

        public static Result Invalid(params Error[] errors)
        {
            return new Result(ResultStatus.Invalid, errors);
        }

        public static Result Unauthorized(params Error[] errors)
        {
            return new Result(ResultStatus.Unauthorized, ErrorResult.Unauthorized(errors));
        }

        public static Result Forbidden(params Error[] errors)
        {
            return new Result(ResultStatus.Forbidden, ErrorResult.Forbidden(errors));
        }

        public static Result Conflict(params Error[] errors)
        {
            return new Result(ResultStatus.Conflict, ErrorResult.Conflict(errors));
        }

        public static Result NotFound(params Error[] errors)
        {
            return new Result(ResultStatus.NotFound, ErrorResult.NotFound(errors));
        }
    }
}