namespace AttendanceTracker.Application.Abstraction.BaseHandlers
{
    internal abstract class CodeGenerationHandler<TRequest> : IHandler<TRequest, string> where TRequest : IRequest<string>
    {
        private static readonly string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private static readonly string _numbers = "1234567890";

        protected static char GetRandomLetter() => _alphabet.ElementAt(Random.Shared.Next(0, _alphabet.Length));

        protected static char GetRandomNumber() => _numbers.ElementAt(Random.Shared.Next(0, _numbers.Length));

        public abstract string HandleRequest(TRequest request);
    }
}
