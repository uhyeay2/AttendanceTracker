namespace AttendanceTracker.Data.Abstraction.BaseRequests
{
    public abstract class CourseCode_DataRequest : IDataRequest
    {
        protected CourseCode_DataRequest(string courseCode) => CourseCode = courseCode;

        public string CourseCode { get; set; }

        public virtual object? GetParameters() => new { CourseCode };

        public abstract string GetSql();
    }

    public abstract class CourseCode_DataRequest<TResponse> : CourseCode_DataRequest, IDataRequest<TResponse>
    {
        protected CourseCode_DataRequest(string courseCode) : base(courseCode) { }
    }
}
