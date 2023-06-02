namespace AttendanceTracker.Domain.Exceptions
{
    public class ValidationFailedException : Exception
    {
        public ValidationFailedException() { }

        public ValidationFailedException(string validationFailureMessage) : this(new List<string>() { validationFailureMessage }) { }

        public List<string> ValidationFailures { get; set; } = new List<string>();

        public ValidationFailedException(List<string> validationFailures) => ValidationFailures = validationFailures;
    }
}
