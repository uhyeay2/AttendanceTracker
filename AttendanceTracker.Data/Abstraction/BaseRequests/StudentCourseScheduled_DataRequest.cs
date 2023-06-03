namespace AttendanceTracker.Data.Abstraction.BaseRequests
{
    public abstract class StudentCourseScheduled_DataRequest : IDataRequest
    {
        public StudentCourseScheduled_DataRequest(string studentCode, Guid courseScheduledGuid)
        {
            StudentCode = studentCode;
            CourseScheduledGuid = courseScheduledGuid;
        }

        public string StudentCode { get; set; }

        public Guid CourseScheduledGuid { get; set; }

        public virtual object? GetParameters() => new { StudentCode, CourseScheduledGuid };

        public abstract string GetSql();
    }

    public abstract class StudentCourseScheduled_DataRequest<TResponse> : StudentCourseScheduled_DataRequest, IDataRequest<TResponse>
    {
        public StudentCourseScheduled_DataRequest(string studentCode, Guid courseScheduledGuid) : base(studentCode, courseScheduledGuid)
        {
        }
    }
}
