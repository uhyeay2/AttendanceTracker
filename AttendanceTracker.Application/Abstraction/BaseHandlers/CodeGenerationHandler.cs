namespace AttendanceTracker.Application.Abstraction.BaseHandlers
{
    internal abstract class CodeGenerationHandler<TRequest> : IHandler<TRequest, string> where TRequest : IRequest<string>
    {
        protected readonly IRandomCharacterFactory _randomCharacterFactory;

        public CodeGenerationHandler(IRandomCharacterFactory randomCharactorFactory) => _randomCharacterFactory = randomCharactorFactory;

        public abstract string HandleRequest(TRequest request);
    }
}
