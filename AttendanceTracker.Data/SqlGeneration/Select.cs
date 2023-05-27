namespace AttendanceTracker.Data.SqlGeneration
{
    public static class Select
    {
        /// <summary>
        /// Generate a Select statement for the table provided. Columns will default to '*' when not provided. 
        /// Defaults to use WITH(NOLOCK). Adds 'WHERE' automatically when provided a where condition.
        /// </summary>
        public static string FromTable(string table, string? columns = null, bool withNoLock = true, string? where = null)
        {
            var select = string.IsNullOrWhiteSpace(columns) ? "SELECT *" : $"SELECT {columns}";

            var fromTable = withNoLock ? $"FROM [dbo].[{table}] WITH(NOLOCK)" : $"FROM [dbo].[{table}]";

            var whereConditionWhenApplicable = string.IsNullOrWhiteSpace(where) ? string.Empty : $" WHERE {where}";

            var sql = $"{select} {fromTable}" + whereConditionWhenApplicable;

            return sql;
        }

        /// <summary>
        /// Sql SELECT statement that will return 1 if record exists, else return 0
        /// </summary>
        public static string Exists(string table, string where, string? column = null, bool withNoLock = true)
        {
            var sql = $"SELECT CASE WHEN EXISTS ( {FromTable(table, column, withNoLock, where)} ) THEN 1 ELSE 0 END";

            return sql;
        }

        public static string JoinFromTable(string table, string joins, string? columns = null, bool withNoLock = true, string? where = null)
        {
            var select = string.IsNullOrWhiteSpace(columns) ? "SELECT *" : $"SELECT {columns}";

            var fromTable = withNoLock ? $"FROM [dbo].[{table}] WITH(NOLOCK)" : $"FROM [dbo].[{table}]";

            var whereConditionWhenApplicable = string.IsNullOrWhiteSpace(where) ? string.Empty : $" WHERE {where}";

            var sql = $"{select} {fromTable} {joins}" + whereConditionWhenApplicable;

            return sql;
        }

        public static string PaginatedFromTable(string table, int pageNumber, int recordsPerPage, string orderBy = "Id", string? columns = null, bool withNoLock = true, string? where = null)
        {
            if (pageNumber < 1) pageNumber = 1;

            if (recordsPerPage < 1) recordsPerPage = 10;

            var select = FromTable(table, columns, withNoLock, where);
            
            var paginated = $" OFFSET {pageNumber - 1} * {recordsPerPage} ROWS FETCH NEXT {recordsPerPage} ROWS ONLY";

            var sql = $"{select} ORDER BY {orderBy} {paginated}";

            return sql;
        }
    }
}
