using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Application.RequestHandlers.InstructorHandlers
{
    public class InsertInstructorRequest : IRequest<Instructor>, IValidatable
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public bool IsValid(out List<string> validationFailures) =>
            Validation.Initialize(out validationFailures)
                .AddFailureIfNullOrWhiteSpace(FirstName, nameof(FirstName))
                .AddFailureIfNullOrWhiteSpace(LastName, nameof(LastName))
            .IsValidWhenNoFailures();
    }

    internal class InsertInstructorHandler : DataOrchestratorHandler<InsertInstructorRequest, Instructor>
    {
        public InsertInstructorHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess, orchestrator) { }

        public override async Task<Instructor> HandleRequestAsync(InsertInstructorRequest request)
        {
            var instructorCode = await _orchestrator.GetResponseAsync<GetUniqueInstructorCodeRequest, string>(new(request.LastName));

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertInstructor(instructorCode, request.FirstName, request.LastName));

            if (rowsAffected.NoRowsAreUpdated())
                throw new ExpectationFailedException(nameof(InsertInstructorRequest));

            var dto = await _dataAccess.FetchAsync(new GetInstructorByCode(instructorCode));

            return new Instructor(dto.InstructorCode, dto.FirstName, dto.LastName);
        }
    }
}
