namespace AttendanceTracker.Data.DataRequestObjects.StudentRequests
{
    public class GetStudentByCode : StudentCode_DataRequest<Student_DTO>
    {
        public GetStudentByCode(string studentCode) : base(studentCode) { }

        public override string GetSql() => Select.FromTable(TableNames.Student, where: "StudentCode = @StudentCode");
    }
}
