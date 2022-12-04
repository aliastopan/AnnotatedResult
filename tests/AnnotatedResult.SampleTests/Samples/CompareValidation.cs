using AnnotatedResult.SampleTests.Models;

namespace AnnotatedResult.SampleTests.Samples;

public static class CompareValidation
{
    public static void Run()
    {
        var model = new ResetPassword()
        {
            NewPassword = "FortisFortunaAdiuvat",
            RepeatPassword = "FortisFortunaAdiuvat"
        };

        Result result;
        var isValid = model.TryValidate(out var errors);
        if(!isValid)
        {
            result = Result.Invalid(errors);
            result.Log();
            return;
        }

        result = Result.Ok();
        Serilog.Log.Information("Status: {0}", result.Status);
    }
}
