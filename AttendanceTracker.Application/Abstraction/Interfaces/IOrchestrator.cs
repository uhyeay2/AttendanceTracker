namespace AttendanceTracker.Application.Abstraction.Interfaces
{
    public interface IOrchestrator
    {
        public Task<TResponse> GetResponseAsync<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>;

        public Task ExecuteRequestAsync<TRequest>(TRequest request) where TRequest : IRequest;

        public TResponse GetResponse<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>;

        public void ExecuteRequest<TRequest>(TRequest request) where TRequest : IRequest;
    }
}
