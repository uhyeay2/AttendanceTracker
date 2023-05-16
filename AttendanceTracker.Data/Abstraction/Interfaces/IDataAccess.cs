namespace AttendanceTracker.Data.Abstraction.Interfaces
{
    public interface IDataAccess
    {
        Task<int> ExecuteAsync(IDataRequest request);

        Task<TResponse> FetchAsync<TResponse>(IDataRequest<TResponse> request);

        Task<IEnumerable<TResponse>> FetchListAsync<TResponse>(IDataRequest<TResponse> request);
    }
}
