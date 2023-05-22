using AttendanceTracker.Data.DataRequestObjects.InstructorRequests;

namespace AttendanceTracker.Application.RequestHandlers.InstructorHandlers
{
    public class DeleteInstructorRequest : RequiredCodeRequest { }

    internal class DeleteInstructorHandler : DataHandler<DeleteInstructorRequest>
    {
        public DeleteInstructorHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task HandleRequestAsync(DeleteInstructorRequest request)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteInstructor(request.Code));

            if (rowsAffected.NoRowsAreUpdated())
            {
                throw await _dataAccess.FetchAsync(new IsInstructorCodeExisting(request.Code)) ?
                    new ExpectationFailedException(nameof(DeleteInstructorRequest))
                    : new DoesNotExistException(typeof(Instructor), (request.Code, nameof(request.Code)));
            }
        }
    }
}
