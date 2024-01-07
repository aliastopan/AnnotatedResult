#nullable disable
using System.Net.Http;
using System.Net.Http.Headers;

namespace AnnotatedResult;

public class HttpResult
{
    public bool IsSuccessStatusCode { get; init; }
    public HttpResponseHeaders Headers { get; init; }
    public string Content { get; init; }
}
