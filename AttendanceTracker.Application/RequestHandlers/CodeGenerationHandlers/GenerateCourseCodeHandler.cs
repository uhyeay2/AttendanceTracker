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
        public override string HandleRequest(GenerateCourseCodeRequest request)
        {
            var randomDigits = new char[CourseCodeConstants.NumberOfEndingDigits];

            var maxLengthForSubjectCode = CourseCodeConstants.MaxLength - CourseCodeConstants.NumberOfEndingDigits;

            if (request.SubjectCode.Length > maxLengthForSubjectCode)
            {
                request.SubjectCode = request.SubjectCode[..maxLengthForSubjectCode];
            }

            for (int i = 0; i < randomDigits.Length; i++)
            {
                randomDigits[i] = GetRandomNumber();
            }

            return request.SubjectCode + new string(randomDigits);
        }
    }
}
