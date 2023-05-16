namespace AttendanceTracker.Domain.Exceptions
{
    public class ValidationFailedException : Exception
    {
        public List<string> ValidationFailures { get; set; }

        public ValidationFailedException(List<string> validationFailures) => ValidationFailures = validationFailures;
    }
}
