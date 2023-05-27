namespace AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests
{
    public class GetStudentCourseScheduled : IDataRequest<CourseScheduled_DTO>
    {
        public GetStudentCourseScheduled(string studentCode, Guid scheduledCourseGuid)
        {
            StudentCode = studentCode;
            ScheduledCourseGuid = scheduledCourseGuid;
        }

        public string StudentCode { get; set; }

        public Guid ScheduledCourseGuid { get; set; }

        public object? GetParameters() => new { StudentCode, ScheduledCourseGuid };

        public string GetSql() => Select.JoinFromTable(
            table: TableNames.Student,
            joins:
            $@"
                LEFT JOIN {TableNames.StudentCourseScheduled} WITH(NOLOCK) ON {TableNames.StudentCourseScheduled}.StudentId = {TableNames.Student}.Id
                LEFT JOIN {TableNames.CourseScheduled} WITH(NOLOCK) ON {TableNames.StudentCourseScheduled}.CourseScheduledId = {TableNames.CourseScheduled}.Id
            ", 
            columns: $"{TableNames.CourseScheduled}.*",
            where:
            $@"
                {TableNames.Student}.StudentCode = @Code AND 
            ");
    }
}
