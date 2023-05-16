namespace AttendanceTracker.Data.Abstraction.BaseRequests
{
    public abstract class StudentCode_DataRequest : IDataRequest
    {
        protected StudentCode_DataRequest(string studentCode) => StudentCode = studentCode;

        public string StudentCode { get; set; }

        public virtual object? GetParameters() => new { StudentCode };

        public abstract string GetSql();
    }

    public abstract class StudentCode_DataRequest<TResponse> : StudentCode_DataRequest, IDataRequest<TResponse>
    {
        protected StudentCode_DataRequest(string studentCode) : base(studentCode) { }
    }
}
