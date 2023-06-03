namespace AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests
{
    public class DeleteStudentCourseScheduled : StudentCourseScheduled_DataRequest
    {
        public DeleteStudentCourseScheduled(string studentCode, Guid courseScheduledGuid) : base(studentCode, courseScheduledGuid) { }

        public override string GetSql() => Delete.FromTable(TableNames.StudentCourseScheduled,
        where: @$"
            CourseScheduledId = ( {Select.FromTable(TableNames.CourseScheduled, "Id", where: "Guid = @CourseScheduledGuid")} )
            AND StudentId = ( {Select.FromTable(TableNames.Student, "Id", where: "StudentCode = @StudentCode")} )
        ");
    }
}
