namespace AttendanceTracker.Domain.Policy.Validation
{
    public static class Validation
    {
        public static bool IsValidWhenNoFailures(this List<string> validationFailures) => !validationFailures.Any();

        public static List<string> Initialize(out List<string> validationFailures)
        {
            validationFailures = new List<string>();

            return validationFailures;
        }
    }
}
