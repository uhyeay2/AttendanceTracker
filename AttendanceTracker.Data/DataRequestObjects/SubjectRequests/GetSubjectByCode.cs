namespace AttendanceTracker.Data.DataRequestObjects.SubjectRequests
{
    public class GetSubjectByCode : Code_DataRequest<Subject_DTO>
    {
        public GetSubjectByCode(string code): base(code) { }

        public override string GetSql() => Select.FromTable(TableNames.Subject, where: "SubjectCode = @Code");
    }
}