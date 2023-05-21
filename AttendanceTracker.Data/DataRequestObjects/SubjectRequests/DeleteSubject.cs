namespace AttendanceTracker.Data.DataRequestObjects.SubjectRequests
{
    public class DeleteSubject : Code_DataRequest
    {
        public DeleteSubject(string code) : base(code) { }

        public override string GetSql() => Delete.FromTable(TableNames.Subject, where: "SubjectCode = @Code");
    }
}