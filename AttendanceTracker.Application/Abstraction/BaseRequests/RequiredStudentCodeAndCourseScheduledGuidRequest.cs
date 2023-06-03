namespace AttendanceTracker.Application.Abstraction.BaseRequests
{   
    public class RequiredStudentCodeAndCourseScheduledGuidRequest : IRequest, IValidatable
    {
        public RequiredStudentCodeAndCourseScheduledGuidRequest() { }

        public RequiredStudentCodeAndCourseScheduledGuidRequest(string studentCode, Guid courseScheduledGuid)
        {
            StudentCode = studentCode;
            CourseScheduledGuid = courseScheduledGuid;
        }

        public string StudentCode { get; set; } = null!;
        public Guid CourseScheduledGuid { get; set; }

        public virtual bool IsValid(out List<string> validationFailures) =>
            Validation.Initialize(out validationFailures)
                .AddFailureIfNullOrWhiteSpace(StudentCode, nameof(StudentCode))
                .AddFailureIfEmpty(CourseScheduledGuid, nameof(CourseScheduledGuid))
            .IsValidWhenNoFailures();

    }

    public class RequiredStudentCodeAndCourseScheduledGuidRequest<TResponse> : RequiredStudentCodeAndCourseScheduledGuidRequest, IRequest<TResponse>
    {
        public RequiredStudentCodeAndCourseScheduledGuidRequest() { }

        public RequiredStudentCodeAndCourseScheduledGuidRequest(string studentCode, Guid courseScheduledGuid) : base(studentCode, courseScheduledGuid) { }
    }
}
