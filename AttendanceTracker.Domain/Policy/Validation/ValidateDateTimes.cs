namespace AttendanceTracker.Domain.Policy.Validation
{
    public static class ValidateDateTimes
    {
        public static List<string> AddFailureIfDateTimeIsMinValue(this List<string> validationFailures, DateTime input, string nameOfInput)
        {
            if (input == DateTime.MinValue)
            {
                validationFailures.Add(ValidationFailureMessage.MissingRequiredField(nameOfInput));
            }

            return validationFailures;
        }
    }
}
