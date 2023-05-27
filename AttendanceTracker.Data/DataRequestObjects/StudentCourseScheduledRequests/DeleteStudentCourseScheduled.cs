namespace AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests
{
    public class DeleteStudentCourseScheduled : IDataRequest
    {
        public DeleteStudentCourseScheduled(string studentCode, Guid courseScheduledGuid)
        {
            StudentCode = studentCode;
            CourseScheduledGuid = courseScheduledGuid;
        }

        public string StudentCode { get; set; }

        public Guid CourseScheduledGuid { get; set; }

        public object? GetParameters() => new {  StudentCode, CourseScheduledGuid };

        public string GetSql() => Delete.FromTable(TableNames.StudentCourseScheduled,
        where: @$"
            CourseScheduledId = ( {Select.FromTable(TableNames.CourseScheduled, "Id", where: "Guid = @CourseScheduledGuid")} )
            AND StudentId = ( {Select.FromTable(TableNames.Student, "Id", where: "StudentCode = @StudentCode")} )
        ");
    }
}
