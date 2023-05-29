using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentHandlers
{
    public class GetStudentsByNameRequest : IRequest<IEnumerable<Student>> , IValidatable
    {
        public GetStudentsByNameRequest() { }

        public GetStudentsByNameRequest(string? firstName = null, string? lastName = null)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public bool IsValid(out List<string> validationFailures) =>
            Validation.Initialize(out validationFailures)
                .AddFailureIfAllAreNullOrWhitespace( (FirstName, nameof(FirstName)), (LastName, nameof(LastName)) )
            .IsValidWhenNoFailures();
    }

    internal class GetStudentsByNameHandler : DataHandler<GetStudentsByNameRequest, IEnumerable<Student>>
    {
        public GetStudentsByNameHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task<IEnumerable<Student>> HandleRequestAsync(GetStudentsByNameRequest request)
        {
            var results = await _dataAccess.FetchListAsync(new GetStudentsByName(request.FirstName, request.LastName));

            return !results?.Any() ?? false ?
                throw new DoesNotExistException(typeof(Student), (request.FirstName, nameof(request.FirstName)), (request.LastName, nameof(request.LastName)) )
                : results!.Select(_ => new Student(_.StudentCode, _.FirstName, _.LastName, _.DateOfBirth));
        }
    }
}
