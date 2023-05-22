namespace AttendanceTracker.Data.DataRequestObjects.InstructorRequests
{
    public class DeleteInstructor : Code_DataRequest
    {
        public DeleteInstructor(string code) : base(code) { }

        public override string GetSql() => Delete.FromTable(TableNames.Instructor, where: "InstructorCode = @Code");
    }
}
