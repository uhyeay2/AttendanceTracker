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
        public GenerateCourseCodeHandler(IRandomCharacterFactory randomCharactorFactory) : base(randomCharactorFactory) { }

        public override string HandleRequest(GenerateCourseCodeRequest request)
        {
            const string hyphen = "-";

            var maxLengthForSubjectCode = CourseCodeConstants.MaxLength - CourseCodeConstants.CountOfEndingNumbers - hyphen.Length;

            if (request.SubjectCode.Length > maxLengthForSubjectCode)
            {
                request.SubjectCode = request.SubjectCode[..maxLengthForSubjectCode];
            }

            var endingNumbers = new char[CourseCodeConstants.CountOfEndingNumbers];
            
            for (int i = 0; i < endingNumbers.Length; i++)
            {
                endingNumbers[i] = _randomCharacterFactory.GetRandomNumber();
            }

            return $"{request.SubjectCode}{hyphen}{new string(endingNumbers)}";
        }
    }
}
