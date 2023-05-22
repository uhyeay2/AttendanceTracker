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

        public static IEnumerable<string> AsSqlParameters(this IEnumerable<string> values)
        {
            if (values == null || !values.Any())
            {
                return Enumerable.Empty<string>();
            }

            return values.Select(v => $"@{v}");
        }

        public static (string columns, string parameters) AggregateWithCommasAsColumnsAndSqlParameters(this string[] columnNames)
        {
            var columns = columnNames.AggregateWithCommas();

            var parameters = columnNames.AsSqlParameters().AggregateWithCommas();

            return (columns, parameters);
        }
    }
}
