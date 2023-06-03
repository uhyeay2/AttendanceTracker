namespace AttendanceTracker.Data.DataRequestObjects.StudentCourseScheduledRequests
{
    public class GetStudentCourseScheduledById : Id_DataRequest<StudentCourseScheduled_DTO>
    {
        public GetStudentCourseScheduledById(int id) : base(id) { }

        public override string GetSql() => 
            Select.JoinFromTable(TableNames.StudentCourseScheduled,
            $"JOIN {TableNames.CourseScheduled} WITH(NOLOCK) ON {TableNames.CourseScheduled}.Id = {TableNames.StudentCourseScheduled}.CourseScheduledId",
            columns: $"{TableNames.CourseScheduled}.*, {TableNames.StudentCourseScheduled}.*", 
            where: $"{TableNames.StudentCourseScheduled}.Id = @Id"
       );
    }
}
