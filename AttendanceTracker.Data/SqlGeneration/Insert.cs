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
            var (columns, parameters) = columnNamesMatchingParameterNames.AggregateWithCommasAsColumnsAndSqlParameters();

            var sql = $"INSERT INTO {table} ( {columns} ) VALUES ( {parameters} )";

            return sql;
        }

        public static string SelectIntoTable(string intoTable, string fromTable, string where, params (string ColumnName, string ValueName)[] columnsAndValues)
        {
            var columns = columnsAndValues.Select(_ => _.ColumnName).AggregateWithCommas();

            var values = columnsAndValues.Select(_ => _.ValueName).AggregateWithCommas();

            var sql = $"INSERT INTO {intoTable} ( {columns} ) SELECT {values} FROM [dbo].[{fromTable}] WITH(NOLOCK) WHERE {where}";

            return sql;
        }
    }
}
