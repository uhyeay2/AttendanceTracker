namespace AttendanceTracker.Data.Abstraction.BaseRequests
{
    public abstract class Code_DataRequest : IDataRequest
    {
        protected Code_DataRequest(string code) => Code = code;

        public string Code { get; set; }

        public virtual object? GetParameters() => new { Code };

        public abstract string GetSql();
    }

    public abstract class Code_DataRequest<TResponse> : Code_DataRequest, IDataRequest<TResponse>
    {
        protected Code_DataRequest(string code) : base(code) { }
    }
}
