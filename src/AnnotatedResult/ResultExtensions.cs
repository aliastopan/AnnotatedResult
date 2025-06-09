using System.Threading.Tasks;

namespace AnnotatedResult
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class ResultExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static Task<Result> AsTask(this Result result)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return Task.FromResult(result);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static Task<Result<T>> AsTask<T>(this Result<T> result)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return Task.FromResult(result);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static Task<Result> AsCompletedTask(this Result result)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return Task.FromResult(result);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static Task<Result<T>> AsCompletedTask<T>(this Result<T> result)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            return Task.FromResult(result);
        }
    }
}