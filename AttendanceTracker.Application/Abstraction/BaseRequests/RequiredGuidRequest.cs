namespace AttendanceTracker.Application.Abstraction.BaseRequests
{
    public abstract class RequiredGuidRequest : IRequest, IValidatable
    {
        protected RequiredGuidRequest(Guid guid) => Guid = guid;

        protected RequiredGuidRequest() { }

        public Guid Guid { get; set; }

        public bool IsValid(out List<string> validationFailures) =>
            Validation.Initialize(out validationFailures)
                .AddFailureIfEmpty(Guid, nameof(Guid))
            .IsValidWhenNoFailures();
    }   

    public abstract class RequiredGuidRequest<TResponse> : RequiredGuidRequest, IRequest<TResponse>
    {
        protected RequiredGuidRequest(Guid guid) : base(guid) { }

        protected RequiredGuidRequest() { }
    }
}
