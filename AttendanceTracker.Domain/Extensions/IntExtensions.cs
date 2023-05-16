namespace AttendanceTracker.Domain.Extensions
{
    public static class IntExtensions
    {
        public static bool AnyRowsAreUpdated(this int rowsAffected) => rowsAffected > 0;

        public static bool NoRowsAreUpdated(this int rowsAffected) => rowsAffected <= 0;
    }
}
