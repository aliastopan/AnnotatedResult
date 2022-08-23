using AnnotatedResult.Common;

namespace AnnotatedResult
{
    public class Result
    {
        internal Result(ResultStatus status)
        {
            Status = status;
        }

        public ResultStatus Status { get; protected set; }
        public bool IsSuccess => Status == ResultStatus.Ok;

        public static Result Ok()
        {
            return new Result(ResultStatus.Ok);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, ResultStatus.Ok);
        }

        public static Result Error()
        {
            return new Result(ResultStatus.Error);
        }
    }
}