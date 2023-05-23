namespace AttendanceTracker.Data.Abstraction.BaseRequests
{
    public abstract class Id_DataRequest : IDataRequest
    {
        public Id_DataRequest(int id) => Id = id;

        public int Id { get; set; }

        public object? GetParameters() => new { Id };

        public abstract string GetSql();
    }

    public abstract class Id_DataRequest<TResponse> : Id_DataRequest, IDataRequest<TResponse>
    {
        protected Id_DataRequest(int id) : base(id) { }
    }
}
