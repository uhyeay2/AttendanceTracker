namespace AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests
{
    public class IsStudentCourseScheduledExisting : IDataRequest<bool>
    {
        public IsStudentCourseScheduledExisting(string studentCode, Guid courseScheduledGuid)
        {
            StudentCode = studentCode;
            CourseScheduledGuid = courseScheduledGuid;
        }

        public string StudentCode { get; set; }

        public Guid CourseScheduledGuid { get; set; }

        public object? GetParameters() => new { StudentCode, CourseScheduledGuid };

        public string GetSql() => Select.Exists(TableNames.StudentCourseScheduled,
            where: $@" 
                StudentId = ( {Select.FromTable(TableNames.Student, "Id", where: "StudentCode = @StudentCode")} )
            AND CourseScheduledId = ( {Select.FromTable(TableNames.CourseScheduled, "Id", where: "Guid = @CourseScheduledGuid")} )
            ");
    }
}
