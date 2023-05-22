using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Application.RequestHandlers.InstructorHandlers
{
    public class UpdateInstructorRequest : RequiredCodeRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    internal class UpdateInstructorHandler : DataHandler<UpdateInstructorRequest>
    {
        public UpdateInstructorHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task HandleRequestAsync(UpdateInstructorRequest request)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new UpdateInstructor(request.Code, request.FirstName, request.LastName));

            if (rowsAffected.NoRowsAreUpdated())
            {
                throw await _dataAccess.FetchAsync(new IsInstructorCodeExisting(request.Code)) ?
                    new ExpectationFailedException(nameof(UpdateInstructorRequest))
                    : new DoesNotExistException(typeof(Instructor), request.Code, nameof(request.Code));
            }
        }
    }
}
