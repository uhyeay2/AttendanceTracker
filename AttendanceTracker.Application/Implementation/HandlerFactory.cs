using AttendanceTracker.Application.Abstraction.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AttendanceTracker.Application.Implementation
{
     internal class HandlerFactory : IHandlerFactory
    {
        private readonly IEnumerable<Type> _handlers;

        private readonly IServiceProvider _serviceProvider;

        public HandlerFactory(IServiceCollection services, IEnumerable<Type> handlers)
        {
            _serviceProvider = services.BuildServiceProvider();

            _handlers = handlers;
        }

        public IHandler<TRequest, TResponse> NewHandler<TRequest, TResponse>() where TRequest : IRequest<TResponse> =>
            Instantiate<TRequest, IHandler<TRequest, TResponse>>();

        public IHandler<TRequest> NewHandler<TRequest>() where TRequest : IRequest =>
            Instantiate<TRequest, IHandler<TRequest>>();

        public ITaskHandler<TRequest> NewTaskHandler<TRequest>() where TRequest : IRequest =>
            Instantiate<TRequest, ITaskHandler<TRequest>>();

        public ITaskHandler<TRequest, TResponse> NewTaskHandler<TRequest, TResponse>() where TRequest : IRequest<TResponse> => 
            Instantiate<TRequest, ITaskHandler<TRequest, TResponse>>();

        private Type GetHandler<TRequest, THandler>() => 
            _handlers.FirstOrDefault(_ => _.IsAssignableTo(typeof(THandler))) 
            ?? throw new Domain.Exceptions.DoesNotExistException(typeNotExisting: typeof(THandler), value: nameof(TRequest), nameOfField: nameof(IRequest));

        private THandler Instantiate<TRequest, THandler>() =>
            (THandler)ActivatorUtilities.CreateInstance(_serviceProvider, GetHandler<TRequest, THandler>());
    }
}
