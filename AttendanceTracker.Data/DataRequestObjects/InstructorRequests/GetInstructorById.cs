namespace AttendanceTracker.Data.DataRequestObjects.InstructorRequests
{
    public class GetInstructorById : Id_DataRequest<Instructor_DTO>
    {
        public GetInstructorById(int id) : base(id) { }

        public override string GetSql() => Select.FromTable(TableNames.Instructor, where: "Id = @Id");
    }
}
