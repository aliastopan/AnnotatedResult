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

        public static Result Error(params string[] errorMessages)
        {
            return new Result(ResultStatus.Error, errorMessages);
        }

        public static Result Invalid(params string[] errorMessages)
        {
            return new Result(ResultStatus.Invalid, errorMessages);
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