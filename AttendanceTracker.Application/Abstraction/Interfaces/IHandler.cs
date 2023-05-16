namespace AttendanceTracker.Application.Abstraction.Interfaces
{
    public interface IHandler { }

    public interface ITaskHandler<TRequest> : IHandler where TRequest : IRequest
    {
        public Task HandleRequestAsync(TRequest request);
    }

    public interface ITaskHandler<TRequest, TResponse> : IHandler where TRequest : IRequest<TResponse>
    {
        public Task<TResponse> HandleRequestAsync(TRequest request);
    }

    public interface IHandler<TRequest, TResponse> : IHandler where TRequest : IRequest<TResponse>
    {
        public TResponse HandleRequest(TRequest request);
    }

    public interface IHandler<TRequest> : IHandler where TRequest : IRequest
    {
        public void HandleRequest(TRequest request);
    }
}
