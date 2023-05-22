namespace AttendanceTracker.Data.Abstraction.BaseRequests
{
    public abstract class Guid_DataRequest : IDataRequest
    {
        protected Guid_DataRequest(Guid guid) => Guid = guid;

        public Guid Guid { get; set; }

        public virtual object? GetParameters() => new { Guid };

        public abstract string GetSql();
    }

    public abstract class Guid_DataRequest<TResponse> : Guid_DataRequest, IDataRequest<TResponse>
    {
        protected Guid_DataRequest(Guid guid) : base(guid) { }
    }
}