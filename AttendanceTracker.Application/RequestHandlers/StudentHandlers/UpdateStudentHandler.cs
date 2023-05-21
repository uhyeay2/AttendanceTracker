using AttendanceTracker.Data.DataRequestObjects.StudentRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentHandlers
{
    public class UpdateStudentRequest : RequiredCodeRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

    internal class UpdateStudentHandler : DataHandler<UpdateStudentRequest>
    {
        public UpdateStudentHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task HandleRequestAsync(UpdateStudentRequest request)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new UpdateStudent(request.Code, request.FirstName, request.LastName, request.DateOfBirth));

            if (rowsAffected.NoRowsAreUpdated())
            {
                throw await _dataAccess.FetchAsync(new IsStudentCodeExisting(request.Code)) ?
                    new ExpectationFailedException(nameof(UpdateStudentRequest))
                    : new DoesNotExistException(typeof(Student), request.Code, nameof(request.Code));
            }
        }
    }
}
