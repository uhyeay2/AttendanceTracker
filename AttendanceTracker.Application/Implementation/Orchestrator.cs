namespace AttendanceTracker.Application.Implementation
{
    internal class Orchestrator : IOrchestrator
    {
        private readonly IHandlerFactory _handlerFactory;

        public Orchestrator(IHandlerFactory handlerFactory) => _handlerFactory = handlerFactory;

        public async Task<TResponse> GetResponseAsync<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
        {
            Validate(request);

            return await _handlerFactory.NewTaskHandler<TRequest, TResponse>().HandleRequestAsync(request);
        }

        public async Task ExecuteRequestAsync<TRequest>(TRequest request) where TRequest : IRequest
        {
            Validate(request);

            await _handlerFactory.NewTaskHandler<TRequest>().HandleRequestAsync(request);
        }

        public TResponse GetResponse<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
        {
            Validate(request);

            return _handlerFactory.NewHandler<TRequest, TResponse>().HandleRequest(request);
        }

        public void ExecuteRequest<TRequest>(TRequest request) where TRequest : IRequest
        {
            Validate(request);

            _handlerFactory.NewHandler<TRequest>().HandleRequest(request);
        }

        private static void Validate(object request)
        {
            if (request is IValidatable validatable && !validatable.IsValid(out var validationFailures))
            {
                throw new ValidationFailedException(validationFailures);
            }
        }
    }
}
