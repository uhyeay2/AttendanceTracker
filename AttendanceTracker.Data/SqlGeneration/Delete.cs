namespace AttendanceTracker.Data.SqlGeneration
{
    public static class Delete
    {
        public static string FromTable(string tableName, string where) => 
            $"DELETE FROM [dbo].[{tableName}] WHERE {where}";
    }
}
