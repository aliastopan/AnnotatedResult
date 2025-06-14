# AnnotatedResult
![Nuget](https://img.shields.io/nuget/v/AnnotatedResult)
![Nuget](https://img.shields.io/nuget/dt/AnnotatedResult?style=flat)
![Nuget](https://img.shields.io/nuget/v/AnnotatedResult.AspNetCore)
![Nuget](https://img.shields.io/nuget/dt/AnnotatedResult.AspNetCore?style=flat)
![GitHub](https://img.shields.io/github/license/aliastopan/AnnotatedResult)

AnnotatedResult is a lightweight .NET model validation library utilizing `System.ComponentModel.DataAnnotations`.

## NuGet Package
```
dotnet add package AnnotatedResult --version 2.1.0-preview.2
dotnet add package AnnotatedResult.AspNetCore --version 2.2.0-preview.2
```

## Overview

### Validation

``` csharp
public class Request
{
    [Required]
    [RegularExpression(RegexPattern.Username)]
    public string Username { get; init; }

    [EmailAddress]
    public string Email { get; init; }

    [Required]
    [RegularExpression(RegexPattern.StrongPassword)]
    public string Password { get; init; }
}
```
`Request` is a class with Data Annotation attribute assigned for each properties. AnnotatedResult make it so it's easy to validate.

``` csharp
    var request = new Request();
    bool isValid = request.TryValidate(out Error[] errors);
```
`TryValidate` is an extension method use to validation any instance of object. If the object has Data Annotation attribute assigned to either field of property it will perform the validation process and return a `boolean` and an `out` parameter of type `Error[]`.

``` csharp
    if(!isValid)
    {
        Result<Request> result = Result<Request>.Invalid(errors);
        return result;
    }

    Result<Request> result = Result<Request>.Ok(request);
    return result;
```
`Result<T>` is a generic wrapper class that encapsulate Data Annotation validation result. The example above it hold the generic type of `Request`. `Result<T>` can be modified whether the is a success or an error--or a specified error such as **Unauthorized** or **NotFound**.

`Result<T>` with 200 status code or `Ok` status return the validated object in question where as error (specified or otherwise) return list of errors.

``` csharp
    var ok           = Result<Request>.Ok(request);

    var error        = Result<Request>.Error(errors);
    var invalid      = Result<Request>.Invalid(errors);
    var unauthorized = Result<Request>.Unauthorized(errors);
    var forbidden    = Result<Request>.Forbidden(errors);
    var conflict     = Result<Request>.Conflict(errors);
    var notFound     = Result<Request>.NotFound(errors);
```


Declaring empty `Result`. By default it return `Result` with the standard `Error`
``` csharp
    var result1 = Result.CreateEmpty();
    var result2 = Result.CreateEmpty("there's nothing inside, empty result");
    var result3 = Result<Request>.CreateEmpty();
    var result4 = Result<Request>.CreateEmpty("there's nothing inside, empty result");
```

