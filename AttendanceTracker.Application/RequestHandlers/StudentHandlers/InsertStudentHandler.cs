using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentHandlers
{
    public class InsertStudentRequest : IRequest<Student>, IValidatable
    {
        public string FirstName { get; set; } = null!;
        
        public string LastName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public bool IsValid(out List<string> validationFailures) =>
            Validation.Initialize(out validationFailures)
                .AddFailureIfNullOrWhiteSpace(FirstName, nameof(FirstName))
                .AddFailureIfNullOrWhiteSpace(LastName, nameof(LastName))
                .AddFailureIfDateTimeIsMinValue(DateOfBirth, nameof(DateOfBirth))
            .IsValidWhenNoFailures();
    }

    internal class InsertStudentHandler : DataOrchestratorHandler<InsertStudentRequest, Student>
    {
        public InsertStudentHandler(IDataAccess dataAccess, IOrchestrator orchestrator) : base(dataAccess, orchestrator) { }

        public override async Task<Student> HandleRequestAsync(InsertStudentRequest request)
        {
            var studentCode = await _orchestrator.GetResponseAsync<GetUniqueStudentCodeRequest, string>(new());

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertStudent(studentCode, request.FirstName, request.LastName, request.DateOfBirth));

            if(rowsAffected.NoRowsAreUpdated())
                throw new ExpectationFailedException(nameof(InsertStudentRequest));

            var student = await _dataAccess.FetchAsync(new GetStudentByCode(studentCode));

            return new Student(student.StudentCode, student.FirstName, student.LastName, student.DateOfBirth);
        }
    }
}
