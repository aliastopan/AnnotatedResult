using System.Net.Http.Headers;

/// <summary>
/// Represents the result of an HTTP operation with additional metadata and error handling.
/// </summary>
public class HttpResult
{
    /// <summary>
    /// Gets a value indicating whether the HTTP request was successful.
    /// </summary>
    public bool IsSuccessStatusCode { get; private set; }

    /// <summary>
    /// Gets the headers associated with the HTTP response.
    /// </summary>
    public HttpResponseHeaders Headers { get; private set; }

    /// <summary>
    /// Gets the content of the HTTP response.
    /// </summary>
    public string Content { get; private set; }
}