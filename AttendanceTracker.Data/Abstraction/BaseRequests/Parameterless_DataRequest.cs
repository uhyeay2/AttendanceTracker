namespace AttendanceTracker.Data.Abstraction.BaseRequests
{
    public abstract class Parameterless_DataRequest : IDataRequest
    {
        public object? GetParameters() => null;
        public abstract string GetSql();
    }

    public abstract class Parameterless_DataRequest<TResponse> : Parameterless_DataRequest, IDataRequest<TResponse> { }
}
