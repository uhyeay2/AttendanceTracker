namespace AttendanceTracker.Domain.Exceptions
{
    public class ValidationFailedException : Exception
    {
        public ValidationFailedException() { }

        public List<string> ValidationFailures { get; set; } = new List<string>();

        public ValidationFailedException(List<string> validationFailures) => ValidationFailures = validationFailures;
    }
}
