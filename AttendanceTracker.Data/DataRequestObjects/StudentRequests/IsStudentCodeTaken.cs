namespace AttendanceTracker.Data.DataRequestObjects.StudentRequests
{
    public class IsStudentCodeTaken : StudentCode_DataRequest<bool>
    {
        public IsStudentCodeTaken(string studentCode) : base(studentCode) { }

        public override string GetSql() => Select.Exists(TableNames.Student, "StudentCode = @StudentCode");
    }
}
