using AttendanceTracker.Domain.Constants;

namespace AttendanceTracker.Application.RequestHandlers.CodeGenerationHandlers
{
    public class GenerateInstructorCodeRequest : CodeGenerationRequest, IValidatable
    {
        public GenerateInstructorCodeRequest() { }

        public GenerateInstructorCodeRequest(string instructorLastName) => InstructorLastName = instructorLastName;

        public string InstructorLastName { get; set; } = string.Empty;

        public bool IsValid(out List<string> validationFailures) =>
            Validation.Initialize(out validationFailures)
                .AddFailureIfNullOrWhiteSpace(InstructorLastName, nameof(InstructorLastName))
            .IsValidWhenNoFailures();
    }

    internal class GenerateInstructorCodeHandler : CodeGenerationHandler<GenerateInstructorCodeRequest>
    {
        public GenerateInstructorCodeHandler(IRandomStringFactory randomStringFactory) : base(randomStringFactory) { }

        public override string HandleRequest(GenerateInstructorCodeRequest request)
        {
            var startingCharacters = request.InstructorLastName[..InstructorCodeConstants.CountOfStartingCharacters].ToUpper();

            var endingNumbers = _randomStringFactory.RandomStringNumbersOnly(InstructorCodeConstants.CountOfEndingNumbers);

            return startingCharacters + endingNumbers;
        }
    }
}
