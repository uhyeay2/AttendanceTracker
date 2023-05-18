using AttendanceTracker.Data.DataRequestObjects.StudentRequests;
using AttendanceTracker.Domain.Policy.CodeGeneration;

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

    internal class InsertStudentHandler : DataTaskHandler<InsertStudentRequest, Student>
    {
        public InsertStudentHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<Student> HandleRequestAsync(InsertStudentRequest request)
        {
            string? studentCode;

            do studentCode = StudentCodeGeneration.NewCode();
                while (await _dataAccess.FetchAsync(new IsStudentCodeTaken(studentCode)));

            var rowsAffected = await _dataAccess.ExecuteAsync(new InsertStudent(studentCode, request.FirstName, request.LastName, request.DateOfBirth));

            if(rowsAffected.NoRowsAreUpdated())
                throw new ExpectationFailedException(nameof(InsertStudentRequest));

            var student = await _dataAccess.FetchAsync(new GetStudentByCode(studentCode));

            return new Student(student.StudentCode, student.FirstName, student.LastName, student.DateOfBirth);
        }
    }
}
