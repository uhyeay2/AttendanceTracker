namespace AttendanceTracker.Data.DataRequestObjects.SubjectRequests
{
    public class InsertSubject : IDataRequest
    {
        public InsertSubject(string subjectCode, string name)
        {
            SubjectCode = subjectCode;
            Name = name;
        }

        public string SubjectCode { get; }
        public string Name { get; }

        public object? GetParameters() => new { SubjectCode, Name };

        public string GetSql() => Insert.IntoTable(TableNames.Subject, "SubjectCode", "Name");
    }
}