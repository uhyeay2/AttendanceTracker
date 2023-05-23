namespace AttendanceTracker.Domain.Policy.Validation
{
    public static class ValidateGuids
    {
        public static List<string> AddFailureIfEmpty(this List<string> validationFailures, Guid input, string nameOfInput)
        {
            if(input == Guid.Empty)
            {
                validationFailures.Add(ValidationFailureMessage.MissingRequiredField(nameOfInput));
            }

            return validationFailures;
        }
    }
}
