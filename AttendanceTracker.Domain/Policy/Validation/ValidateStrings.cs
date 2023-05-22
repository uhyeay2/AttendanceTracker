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

        public static List<string> AddFailureIfOutsideRange(this List<string> validationFailures, string? input, string nameOfInput, int? minLength = null, int? maxLength = null)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                validationFailures.Add(ValidationFailureMessage.MissingRequiredField(nameOfInput));
                return validationFailures;
            }

            if(minLength.HasValue && input.Length < minLength)
            {
                validationFailures.Add(ValidationFailureMessage.MustMeetMinimumLengthRequirement(nameOfInput, minLength.Value, input.Length));
            }

            if (maxLength.HasValue && input.Length > maxLength)
            {
                validationFailures.Add(ValidationFailureMessage.MustMeetMaximumLengthRequirement(nameOfInput, maxLength.Value, input.Length));
            }

            return validationFailures;
        }

        public static List<string> AddFailureIfAnyCharactersAreNotLetters(this List<string> validationFailures, string? input, string nameOfInput)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                validationFailures.Add(ValidationFailureMessage.MissingRequiredField(nameOfInput));
                return validationFailures;
            }

            if(input.Any(_ => !char.IsLetter(_)))
            {
                validationFailures.Add(ValidationFailureMessage.MustBeLettersOnly(input, nameOfInput));
            }

            return validationFailures;
        }
    }
}
