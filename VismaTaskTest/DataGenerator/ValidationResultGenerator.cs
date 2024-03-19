using FluentValidation.Results;

namespace VismaTaskTests.DataGenerator;

public static class ValidationResultGenerator
{
    public static ValidationResult Get(bool hasErrors)
    {
        var validationResult = new ValidationResult();

        if (hasErrors)
        {
            var validationError = new ValidationFailure("error", "errror");
            validationResult.Errors.Add(validationError);
        }

        return validationResult;
    }
}
