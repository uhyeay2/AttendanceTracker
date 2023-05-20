namespace AttendanceTracker.Domain.Constants
{
    public static class CourseCodeConstants
    {
        public const int NumberOfEndingNumbers = 6;

        public const int MaxLength = 15;

        public const int MaxAttemptsToGenerate = 3;

        public const string MaxAttemptsExceededErrorMessage = "Exceeded Max Attempts To Generate Course Code";
    }
}
