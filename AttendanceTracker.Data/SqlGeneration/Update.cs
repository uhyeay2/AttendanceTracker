using AttendanceTracker.Domain.Extensions;

namespace AttendanceTracker.Data.SqlGeneration
{
    public static class Update
    {
        public static string CoalesceTable(string table, string where, params (string columnName, string valueName)[] columnsAndValues)
        {
            var fieldsToUpdate = columnsAndValues
                .Select(_ => $"{_.columnName} = COALESCE({_.valueName}, {_.columnName})")
                .AggregateWithCommas();

            var sql = $"UPDATE [dbo].[{table}] SET {fieldsToUpdate} WHERE {where}";

            return sql; 
        }

        public static string CoalesceTable(string table, string where, params string[] columnNamesMatchingParameterNames)
        {
            var fieldsToUpdate = columnNamesMatchingParameterNames
                .Select(v => $"{v} = COALESCE(@{v}, {v})")
                .AggregateWithCommas();

            var sql = $"UPDATE [dbo].[{table}] SET {fieldsToUpdate} WHERE {where}";

            return sql;
        }
    }
}
