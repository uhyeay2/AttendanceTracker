using AttendanceTracker.Domain.Constants;

namespace AttendanceTracker.Application.RequestHandlers.CodeGenerationHandlers
{
    public class GenerateCourseCodeRequest : CodeGenerationRequest, IValidatable
    {
        public GenerateCourseCodeRequest(string courseSubjectCode) => CourseSubjectCode = courseSubjectCode;

        public string CourseSubjectCode { get; set; } = null!;

        public bool IsValid(out List<string> validationFailures) => 
            Validation.Initialize(out validationFailures)
                .AddFailureIfNullOrWhiteSpace(CourseSubjectCode, nameof(CourseSubjectCode))
            .IsValidWhenNoFailures();
    }

    internal class GenerateCourseCodeHandler : CodeGenerationHandler<GenerateCourseCodeRequest>
    {
        public override string HandleRequest(GenerateCourseCodeRequest request)
        {
            var randomDigits = new char[CourseCodeConstants.NumberOfEndingDigits];

            for (int i = 0; i < randomDigits.Length; i++)
            {
                randomDigits[i] = GetRandomNumber();
            }

            return request.CourseSubjectCode + new string(randomDigits);
        }
    }
}
