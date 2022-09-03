using System.Threading.Tasks;
namespace AnnotatedResult
{
    public static class ResultExtensions
    {
        public static Task<Result> AsTask(this Result result)
        {
            return Task.FromResult(result);
        }

        public static Task<Result<T>> AsTask<T>(this Result<T> result)
        {
            return Task.FromResult(result);
        }
    }
}