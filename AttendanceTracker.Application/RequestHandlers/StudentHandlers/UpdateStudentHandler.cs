using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentHandlers
{
    public class UpdateStudentRequest : RequiredStudentCodeRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

    internal class UpdateStudentHandler : DataTaskHandler<UpdateStudentRequest>
    {
        public UpdateStudentHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task HandleRequestAsync(UpdateStudentRequest request)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new UpdateStudent(request.StudentCode, request.FirstName, request.LastName, request.DateOfBirth));

            if (rowsAffected.NoRowsAreUpdated())
            {
                throw await _dataAccess.FetchAsync(new IsStudentCodeTaken(request.StudentCode)) ?
                    new ExpectationFailedException(nameof(UpdateStudentRequest))
                    : new DoesNotExistException(typeof(Student), request.StudentCode, nameof(request.StudentCode));
            }
        }
    }
}
