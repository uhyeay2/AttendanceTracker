using AttendanceTracker.Application.RequestHandlers.CodeGenerationHandlers;
using AttendanceTracker.Data.DataRequestObjects.CourseRequests;
using AttendanceTracker.Domain.Constants;

namespace AttendanceTracker.Application.RequestHandlers.CourseHandlers
{
    public class GetUniqueCourseCodeRequest : IRequest<string>, IValidatable
    {
        public GetUniqueCourseCodeRequest(string subjectCode) =>  SubjectCode = subjectCode;

        public GetUniqueCourseCodeRequest() { }

        public string SubjectCode { get; set; } = null!;

        public bool IsValid(out List<string> validationFailures) =>
            Validation.Initialize(out validationFailures)
                .AddFailureIfNullOrWhiteSpace(SubjectCode, nameof(SubjectCode))
            .IsValidWhenNoFailures();
    }

    internal class GetUniqueCourseCodeHandler : DataOrchestratorHandler<GetUniqueCourseCodeRequest, string>
    {
        public GetUniqueCourseCodeHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess, orchestrator) { }

        public override async Task<string> HandleRequestAsync(GetUniqueCourseCodeRequest request)
        {
            for (int i = 0; i < CourseCodeConstants.MaxAttemptsToGenerate; i++)
            {
                var code = _orchestrator.GetResponse<GenerateCourseCodeRequest, string>(new(request.SubjectCode));

                if (!await _dataAccess.FetchAsync(new IsCourseCodeExisting(code)))
                {
                    return code;
                }
            }

            throw new ExpectationFailedException(nameof(request), CourseCodeConstants.MaxAttemptsExceededErrorMessage);
        }
    }
}
