namespace AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests
{
    public class InsertStudentCourseScheduled : IDataRequest
    {
        public InsertStudentCourseScheduled(string studentCode, Guid courseScheduledGuid)
        {
            StudentCode = studentCode;
            CourseScheduledGuid = courseScheduledGuid;
        }

        public string StudentCode { get; set; }

        public Guid CourseScheduledGuid { get; set; }

        public object? GetParameters() => new {  StudentCode, CourseScheduledGuid };

        public string GetSql() => 
        $@"
            DECLARE @StudentId INT = ( {Select.FromTable(TableNames.Student, "Id", where: "StudentCode = @StudentCode")} )

            DECLARE @CourseScheduledId INT = ( {Select.FromTable(TableNames.CourseScheduled, "Id", where: "Guid = @CourseScheduledGuid")} )

            IF @StudentId IS NOT NULL 
            AND @CourseScheduledId IS NOT NULL 
            AND ( {Select.Exists(TableNames.StudentCourseScheduled, 
                          where: "StudentId = @StudentId AND CourseScheduledId = @CourseScheduledId")} ) = 0
            BEGIN
                {Insert.IntoTable(TableNames.StudentCourseScheduled, "StudentId", "CourseScheduledId")}
            END
        ";
    }
}
