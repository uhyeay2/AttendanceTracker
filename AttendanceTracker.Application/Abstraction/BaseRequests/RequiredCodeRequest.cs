namespace AttendanceTracker.Application.Abstraction.BaseRequests
{
    public abstract class RequiredCodeRequest : IRequest, IValidatable
    {
        protected RequiredCodeRequest() { }

        protected RequiredCodeRequest(string code) => Code = code;

        public string Code { get; set; } = null!;

        public bool IsValid(out List<string> validationFailures) =>
            Validation.Initialize(out validationFailures)
                .AddFailureIfNullOrWhiteSpace(Code, nameof(Code))
            .IsValidWhenNoFailures();
    }

    public abstract class RequiredCodeRequest<TResponse> : RequiredCodeRequest, IRequest<TResponse>
    {
        protected RequiredCodeRequest() { }

        protected RequiredCodeRequest(string code) : base(code) { }
    }
}
