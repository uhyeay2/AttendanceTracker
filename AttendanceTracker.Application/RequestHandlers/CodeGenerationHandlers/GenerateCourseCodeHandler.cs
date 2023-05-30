using AttendanceTracker.Domain.Constants;

namespace AttendanceTracker.Application.RequestHandlers.CodeGenerationHandlers
{
    public class GenerateCourseCodeRequest : CodeGenerationRequest, IValidatable
    {
        public GenerateCourseCodeRequest(string courseSubjectCode) => SubjectCode = courseSubjectCode;

        public string SubjectCode { get; set; } = null!;

        public bool IsValid(out List<string> validationFailures) => 
            Validation.Initialize(out validationFailures)
                .AddFailureIfNullOrWhiteSpace(SubjectCode, nameof(SubjectCode))
            .IsValidWhenNoFailures();
    }

    internal class GenerateCourseCodeHandler : CodeGenerationHandler<GenerateCourseCodeRequest>
    {
        public GenerateCourseCodeHandler(IRandomStringFactory randomStringFactory) : base(randomStringFactory) { }

        public override string HandleRequest(GenerateCourseCodeRequest request)
        {
            var endingNumbers = _randomStringFactory.RandomStringNumbersOnly(CourseCodeConstants.CountOfEndingNumbers);

            return $"{request.SubjectCode}-{endingNumbers}";
        }
    }
}
