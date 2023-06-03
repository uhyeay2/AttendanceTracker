namespace AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests
{
    public class GetStudentCourseScheduled : IDataRequest<StudentCourseScheduled_DTO>
    {
        public GetStudentCourseScheduled(string studentCode, Guid courseScheduledGuid)
        {
            StudentCode = studentCode;
            CourseScheduledGuid = courseScheduledGuid;
        }

        public string StudentCode { get; set; }

        public Guid CourseScheduledGuid { get; set; }

        public object? GetParameters() => new { StudentCode, CourseScheduledGuid };

        public string GetSql() => Select.JoinFromTable(
            table: TableNames.Student,
            joins:
            $@"
                LEFT JOIN {TableNames.StudentCourseScheduled} WITH(NOLOCK) ON {TableNames.StudentCourseScheduled}.StudentId = {TableNames.Student}.Id
                LEFT JOIN {TableNames.CourseScheduled} WITH(NOLOCK) ON {TableNames.StudentCourseScheduled}.CourseScheduledId = {TableNames.CourseScheduled}.Id
            ", 
            columns: $"{TableNames.CourseScheduled}.*, {TableNames.StudentCourseScheduled}.*",
            where: $"{TableNames.Student}.StudentCode = @StudentCode AND {TableNames.CourseScheduled}.Guid = @CourseScheduledGuid"
        );
    }
}
