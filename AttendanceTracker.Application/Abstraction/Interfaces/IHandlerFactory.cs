namespace AttendanceTracker.Application.Abstraction.Interfaces
{
    internal interface IHandlerFactory
    {
        public ITaskHandler<TRequest, TResponse> NewTaskHandler<TRequest, TResponse>() where TRequest : IRequest<TResponse>;

        public ITaskHandler<TRequest> NewTaskHandler<TRequest>() where TRequest : IRequest;

        public IHandler<TRequest, TResponse> NewHandler<TRequest, TResponse>() where TRequest : IRequest<TResponse>;

        public IHandler<TRequest> NewHandler<TRequest>() where TRequest : IRequest;
    }
}
