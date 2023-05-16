namespace AttendanceTracker.Domain.Policy.Validation
{
    public static class ValidateStrings
    {
        public static List<string> AddFailureIfNullOrWhiteSpace(this List<string> validationFailures, string? input, string nameOfInput)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                validationFailures.Add(ValidationFailureMessage.MissingRequiredField(nameOfInput));
            }

            return validationFailures;
        }

        public static List<string> AddFailureIfAllAreNullOrWhitespace(this List<string> validationFailures, params (string? input, string nameOfInput)[] inputs)
        {
            if (inputs.All(_ => string.IsNullOrWhiteSpace(_.input)))
            {
                validationFailures.Add(ValidationFailureMessage.MissingOneOfAnyRequiredFields(inputs.Select(_ => _.nameOfInput)));
            }

            return validationFailures;
        }
    }
}
