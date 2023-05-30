namespace AttendanceTracker.Application.Abstraction.BaseHandlers
{
    internal abstract class CodeGenerationHandler<TRequest> : IHandler<TRequest, string> where TRequest : IRequest<string>
    {
        protected readonly IRandomStringFactory _randomStringFactory;

        public CodeGenerationHandler(IRandomStringFactory randomStringFactory) => _randomStringFactory = randomStringFactory;

        public abstract string HandleRequest(TRequest request);
    }
}
