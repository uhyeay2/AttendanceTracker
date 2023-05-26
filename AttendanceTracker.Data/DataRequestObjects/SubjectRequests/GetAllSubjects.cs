namespace AttendanceTracker.Data.DataRequestObjects.SubjectRequests
{
    public class GetAllSubjects : Parameterless_DataRequest<Subject_DTO>
    {
        public override string GetSql() => Select.FromTable(TableNames.Subject);
    }
}
