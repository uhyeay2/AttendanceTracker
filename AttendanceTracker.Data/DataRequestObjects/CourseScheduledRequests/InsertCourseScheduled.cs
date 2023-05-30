namespace AttendanceTracker.Data.DataRequestObjects.CourseScheduledRequests
{
    public class InsertCourseScheduled : IDataRequest
    {
        public InsertCourseScheduled(Guid guid, string courseCode, string instructorCode, DateTime startDate, DateTime endDate)
        {
            Guid = guid;
            CourseCode = courseCode;
            InstructorCode = instructorCode;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Guid Guid { get; set; }
        public string CourseCode { get; set; }
        public string InstructorCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public object? GetParameters() => this;

        public string GetSql() =>
        $@"
            DECLARE @CourseId INT = ( {Select.FromTable(TableNames.Course, "Id", where: "CourseCode = @CourseCode")} )
            DECLARE @InstructorId INT = ( {Select.FromTable(TableNames.Instructor, "Id", where: "InstructorCode = @InstructorCode")} )

            IF @CourseId IS NOT NULL AND @InstructorId IS NOT NULL
            {Insert.IntoTable(TableNames.CourseScheduled, "Guid", "CourseId", "InstructorId", "StartDate", "EndDate")}
        ";
    }
}
