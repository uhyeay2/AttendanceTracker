namespace AttendanceTracker.Data.DataRequestObjects.StudentRequests
{
    public class DeleteStudent : Code_DataRequest
    {
        public DeleteStudent(string studentCode) : base(studentCode) { }

        public override string GetSql() => Delete.FromTable(TableNames.Student, where: "StudentCode = @Code");
    }
}
