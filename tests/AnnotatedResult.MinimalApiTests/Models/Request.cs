namespace AnnotatedResult.MinimalApiTests.Models;

public record Request
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }

    public Request(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
