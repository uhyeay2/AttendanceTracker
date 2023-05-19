namespace AttendanceTracker.Domain.Constants
{
    public static class StudentCodeConstants
    {
        public const int LengthOfLeadingLetters = 3;

        public static int ExpectedLength => 7;

        public const int MaxAttemptsToGenerate = 5;

        public const string MaxAttemptsExceededErrorMessage = "Exceeded Max Attempts To Generate Student Code";
    }
}
