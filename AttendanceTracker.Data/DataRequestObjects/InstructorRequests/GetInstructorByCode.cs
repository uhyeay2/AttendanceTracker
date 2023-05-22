namespace AttendanceTracker.Data.DataRequestObjects.InstructorRequests
{
    public class GetInstructorByCode : Code_DataRequest<Instructor_DTO>
    {
        public GetInstructorByCode(string code) : base(code) { }

        public override string GetSql() => Select.FromTable(TableNames.Instructor, where: "InstructorCode = @Code");
    }
}
