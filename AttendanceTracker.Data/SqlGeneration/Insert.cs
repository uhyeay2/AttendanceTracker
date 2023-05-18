using AttendanceTracker.Domain.Extensions;

namespace AttendanceTracker.Data.SqlGeneration
{
    public static class Insert
    {
        public static string IntoTable(string table, params (string ColumnName, string ValueName)[] columnsAndValues)
        {
            var columns = columnsAndValues.Select(_ => _.ColumnName).AggregateWithCommas();

            var values = columnsAndValues.Select(_ => _.ValueName).AggregateWithCommas();

            var sql = $"INSERT INTO {table} ( {columns} ) VALUES ( {values} )";

            return sql;
        }

        public static string IntoTable(string table, params string[] columnNamesMatchingParameterNames)
        {
            var columns = columnNamesMatchingParameterNames.AggregateWithCommas();

            var values = columnNamesMatchingParameterNames.Select(n => "@" + n).AggregateWithCommas();

            var sql = $"INSERT INTO {table} ( {columns} ) VALUES ( {values} )";

            return sql;
        }
    }
}
