using AttendanceTracker.Application.RequestHandlers.CodeGenerationHandlers;
using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;
using AttendanceTracker.Domain.Constants;

namespace AttendanceTracker.Application.RequestHandlers.InstructorHandlers
{
    public class GetUniqueInstructorCodeRequest : CodeGenerationRequest, IValidatable
    {
        public GetUniqueInstructorCodeRequest() { }

        public GetUniqueInstructorCodeRequest(string instructorLastName) => InstructorLastName = instructorLastName;

        public string InstructorLastName { get; set; } = string.Empty;

        public bool IsValid(out List<string> validationFailures) =>
           Validation.Initialize(out validationFailures)
               .AddFailureIfNullOrWhiteSpace(InstructorLastName, nameof(InstructorLastName))
           .IsValidWhenNoFailures();
    }

    internal class GetUniqueInstructorCodeHandler : DataOrchestratorHandler<GetUniqueInstructorCodeRequest, string>
    {
        public GetUniqueInstructorCodeHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess, orchestrator) { }

        public override async Task<string> HandleRequestAsync(GetUniqueInstructorCodeRequest request)
        {
            for (int i = 0; i < InstructorCodeConstants.MaxAttemptsToGenerate; i++)
            {
                var code = _orchestrator.GetResponse<GenerateInstructorCodeRequest, string>(new GenerateInstructorCodeRequest(request.InstructorLastName));

                if (!await _dataAccess.FetchAsync(new IsInstructorCodeExisting(code)))
                {
                    return code;
                }
            }

            throw new ExpectationFailedException(nameof(request), InstructorCodeConstants.MaxAttemptsExceededErrorMessage);
        }
    }
}
