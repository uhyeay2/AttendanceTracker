namespace AttendanceTracker.Domain.Constants
{
    public static class StudentCodeConstants
    {
        public const int LengthOfLeadingLetters = 3;

        public const int LengthOfEndingNumbers = 4;

        public static int ExpectedLength => LengthOfLeadingLetters + LengthOfEndingNumbers;
    }
}
