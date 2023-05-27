namespace AttendanceTracker.Domain.Policy.Validation
{
    public static class ValidateInts
    {
        public static List<string> AddFailureIfOutsideRange(this List<string> validationFailures, int? input, string nameOfInput, int? minLength = null, int? maxLength = null)
        {
            if (input == null)
            {
                validationFailures.Add(ValidationFailureMessage.MissingRequiredField(nameOfInput));
                return validationFailures;
            }

            if (minLength.HasValue && input < minLength)
            {
                validationFailures.Add(ValidationFailureMessage.MustMeetMinimumLengthRequirement(nameOfInput, minLength.Value, input.Value));
            }

            if (maxLength.HasValue && input > maxLength)
            {
                validationFailures.Add(ValidationFailureMessage.MustMeetMaximumLengthRequirement(nameOfInput, maxLength.Value, input.Value));
            }

            return validationFailures;
        }
    }
}
