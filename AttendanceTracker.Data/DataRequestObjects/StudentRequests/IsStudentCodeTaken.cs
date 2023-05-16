namespace AttendanceTracker.Data.DataRequestObjects.StudentRequests
{
    public class IsStudentCodeTaken : StudentCode_DataRequest<bool>
    {
        public IsStudentCodeTaken(string studentCode) : base(studentCode) { }

        public override string GetSql() => "SELECT CASE WHEN EXISTS (SELECT * FROM Student WHERE StudentCode = @StudentCode) THEN 1 ELSE 0 END";
    }
}
