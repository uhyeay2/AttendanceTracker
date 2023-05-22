namespace AttendanceTracker.Domain.Constants
{
    public static class InstructorCodeConstants
    {
        public const int MaxLength = 7;

        public const int CountOfEndingNumbers = 4;

        public const int CountOfStartingCharacters = 3;

        public const int MaxAttemptsToGenerate = 3;

        public const string MaxAttemptsExceededErrorMessage = "Exceeded Max Attempts To Generate Instructor Code";
    }
}
