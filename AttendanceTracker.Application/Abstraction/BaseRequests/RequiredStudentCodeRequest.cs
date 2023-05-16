namespace AttendanceTracker.Application.Abstraction.BaseRequests
{
    public abstract class RequiredStudentCodeRequest : IRequest, IValidatable
    {
        protected RequiredStudentCodeRequest() { }

        protected RequiredStudentCodeRequest(string studentCode) =>StudentCode = studentCode;

        public string StudentCode { get; set; } = null!;

        public bool IsValid(out List<string> validationFailures) =>
            Validation.Initialize(out validationFailures)
                .AddFailureIfNullOrWhiteSpace(StudentCode, nameof(StudentCode))
            .IsValidWhenNoFailures();
    }

    public abstract class RequiredStudentCodeRequest<TResponse> : RequiredStudentCodeRequest, IRequest<TResponse>
    {
        protected RequiredStudentCodeRequest() { }

        protected RequiredStudentCodeRequest(string studentCode) : base(studentCode) { }
    }
}
