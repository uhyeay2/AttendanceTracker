namespace AttendanceTracker.Data.Abstraction.Interfaces
{
    public interface IDataRequest
    {
        string GetSql();

        object? GetParameters();
    }

    public interface IDataRequest<TResponse> : IDataRequest { }
}
