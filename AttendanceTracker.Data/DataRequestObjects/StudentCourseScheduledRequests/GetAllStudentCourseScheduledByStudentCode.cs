namespace AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests
{
    public class GetAllStudentCourseScheduledByStudentCode : Code_DataRequest<CourseScheduled_DTO>
    {
        public GetAllStudentCourseScheduledByStudentCode(string code) : base(code) { }

        public override string GetSql() => 
            Select.JoinFromTable(TableNames.StudentCourseScheduled, 
            joins: 
            $@"
                LEFT JOIN {TableNames.Student} WITH(NOLOCK) ON {TableNames.StudentCourseScheduled}.StudentId = {TableNames.Student}.Id
                LEFT JOIN {TableNames.CourseScheduled} WITH(NOLOCK) ON {TableNames.StudentCourseScheduled}.CourseScheduledId = {TableNames.CourseScheduled}.Id
            ", columns: "CourseScheduled.*",
            where: 
            $@"
                {TableNames.Student}.StudentCode = @Code
            ");
    }
}
