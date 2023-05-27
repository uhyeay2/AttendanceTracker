namespace AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests
{
    public class GetStudentCoursesScheduledByStudentCode : Code_DataRequest<CourseScheduled_DTO>
    {
        public GetStudentCoursesScheduledByStudentCode(string code) : base(code) { }

        public override string GetSql() => 
            Select.JoinFromTable(TableNames.Student, 
            joins: 
            $@"
                LEFT JOIN {TableNames.StudentCourseScheduled} WITH(NOLOCK) ON {TableNames.StudentCourseScheduled}.StudentId = {TableNames.Student}.Id
                LEFT JOIN {TableNames.CourseScheduled} WITH(NOLOCK) ON {TableNames.StudentCourseScheduled}.CourseScheduledId = {TableNames.CourseScheduled}.Id
            ", columns: "CourseScheduled.*",
            where: 
            $@"
                {TableNames.Student}.StudentCode = @Code
            ");
    }
}
