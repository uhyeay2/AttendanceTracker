namespace AttendanceTracker.Data.DataRequestObjects.InstructorRequests
{
    public class IsInstructorCodeExisting : Code_DataRequest<bool>
    {
        public IsInstructorCodeExisting(string code) : base(code) { }

        public override string GetSql() => Select.Exists(TableNames.Instructor, where: "InstructorCode = @Code");
    }
}
