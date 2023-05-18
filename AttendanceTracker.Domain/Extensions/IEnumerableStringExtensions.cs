namespace AttendanceTracker.Domain.Extensions
{
    public static class IEnumerableStringExtensions
    {
        public static string AggregateWithCommas(this IEnumerable<string> values)
        {
            if (values == null || !values.Any())
            {
                return string.Empty;
            }

            return values.Aggregate((a, b) => $"{a}, {b}");
        }
    }
}
