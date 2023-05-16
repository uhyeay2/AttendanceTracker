namespace AttendanceTracker.Data.DataRequestObjects.StudentRequests
{
    public class DeleteStudent : StudentCode_DataRequest
    {
        public DeleteStudent(string studentCode) : base(studentCode) { }

        public override string GetSql() => "DELETE FROM [dbo].[Student] WHERE StudentCode = @StudentCode";
    }
}
