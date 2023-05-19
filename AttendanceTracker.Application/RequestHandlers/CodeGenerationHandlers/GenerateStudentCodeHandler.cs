using AttendanceTracker.Domain.Constants;

namespace AttendanceTracker.Application.RequestHandlers.CodeGenerationHandlers
{
    public class GenerateStudentCodeRequest : CodeGenerationRequest { }

    internal class GenerateStudentCodeHandler : CodeGenerationHandler<GenerateStudentCodeRequest>
    {
        public override string HandleRequest(GenerateStudentCodeRequest request)
        {
            var code = new char[StudentCodeConstants.ExpectedLength];

            for (int i = 0; i < code.Length; i++)
            {
                code[i] = i < StudentCodeConstants.LengthOfLeadingLetters ? GetRandomLetter() : GetRandomNumber();
            }

            return new(code);
        }
    }
}
