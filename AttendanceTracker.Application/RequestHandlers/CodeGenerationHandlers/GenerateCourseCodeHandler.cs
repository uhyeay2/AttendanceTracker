﻿using AttendanceTracker.Domain.Constants;

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
            var endingNumbers = new char[CourseCodeConstants.CountOfEndingNumbers];
            
            for (int i = 0; i < endingNumbers.Length; i++)
            {
                endingNumbers[i] = _randomCharacterFactory.GetRandomNumber();
            }

            return $"{request.SubjectCode}-{new string(endingNumbers)}";
        }
    }
}
