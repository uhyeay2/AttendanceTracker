namespace AttendanceTracker.Data.DataRequestObjects.StudentRequests
{
    public class IsStudentCodeExisting : Code_DataRequest<bool>
    {
        public IsStudentCodeExisting(string studentCode) : base(studentCode) { }

        public override string GetSql() => Select.Exists(TableNames.Student, where: "StudentCode = @Code");
    }
}
