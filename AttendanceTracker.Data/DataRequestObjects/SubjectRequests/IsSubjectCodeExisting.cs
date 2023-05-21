namespace AttendanceTracker.Data.DataRequestObjects.SubjectRequests
{
    public class IsSubjectCodeExisting : Code_DataRequest<bool>
    {
        public IsSubjectCodeExisting(string code) : base(code) { }

        public override string GetSql() => Select.Exists(TableNames.Subject, where: "SubjectCode = @Code");
    }
}