using AttendanceTracker.Application.RequestHandlers.CodeGenerationHandlers;
using AttendanceTracker.Data.DataRequestObjects.StudentRequests;
using AttendanceTracker.Domain.Constants;

namespace AttendanceTracker.Application.RequestHandlers.StudentHandlers
{
    public class GetUniqueStudentCodeRequest : CodeGenerationRequest { }

    internal class GetUniqueStudentCodeHandler : DataOrchestratorHandler<GetUniqueStudentCodeRequest, string>
    {
        public GetUniqueStudentCodeHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess, orchestrator) { }

        public override async Task<string> HandleRequestAsync(GetUniqueStudentCodeRequest request)
        {
            for (int i = 0; i < StudentCodeConstants.MaxAttemptsToGenerate; i++)
            {
                var code = _orchestrator.GetResponse<GenerateStudentCodeRequest, string>(new GenerateStudentCodeRequest());

                if(!await _dataAccess.FetchAsync(new IsStudentCodeTaken(code)))
                {
                    return code;
                }
            }

            throw new ExpectationFailedException(nameof(request), StudentCodeConstants.MaxAttemptsExceededErrorMessage);
        }
    }
}
