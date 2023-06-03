namespace AttendanceTracker.Data.DataRequestObjects.StudentRequests
{
    public class GetStudentById : Id_DataRequest<Student_DTO>
    {
        public GetStudentById(int id) : base(id) { }

        public override string GetSql() => Select.FromTable(TableNames.Student, where: "Id = @Id");
    }
}
