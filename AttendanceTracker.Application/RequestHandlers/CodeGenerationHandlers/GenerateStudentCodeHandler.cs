using AttendanceTracker.Domain.Constants;

namespace AttendanceTracker.Application.RequestHandlers.CodeGenerationHandlers
{
    public class GenerateStudentCodeRequest : CodeGenerationRequest { }

    internal class GenerateStudentCodeHandler : CodeGenerationHandler<GenerateStudentCodeRequest>
    {
        public GenerateStudentCodeHandler(IRandomStringFactory randomStringFactory) : base(randomStringFactory) { }

        public override string HandleRequest(GenerateStudentCodeRequest request)
        {
            var leadingLetters = _randomStringFactory.RandomStringLettersOnly(StudentCodeConstants.LengthOfLeadingLetters);

            var endingNumbers = _randomStringFactory.RandomStringNumbersOnly(StudentCodeConstants.ExpectedLength - StudentCodeConstants.LengthOfLeadingLetters);

            return leadingLetters + endingNumbers;
        }
    }
}
