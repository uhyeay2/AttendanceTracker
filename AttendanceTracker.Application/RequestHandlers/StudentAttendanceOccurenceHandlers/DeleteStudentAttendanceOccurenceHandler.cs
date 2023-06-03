using AttendanceTracker.Data.DataRequestObjects.StudentAttendanceOccurenceRequests;

namespace AttendanceTracker.Application.RequestHandlers.StudentAttendanceOccurenceHandlers
{
    public class DeleteStudentAttendanceOccurenceRequest : RequiredGuidRequest
    {
        public DeleteStudentAttendanceOccurenceRequest() { }

        public DeleteStudentAttendanceOccurenceRequest(Guid guid) : base(guid) { }
    }

    internal class DeleteStudentAttendanceOccurenceHandler : DataHandler<DeleteStudentAttendanceOccurenceRequest>
    {
        public DeleteStudentAttendanceOccurenceHandler(IDataAccess dataAccess) : base(dataAccess) { }

        public override async Task HandleRequestAsync(DeleteStudentAttendanceOccurenceRequest request)
        {
            var rowsAffected = await _dataAccess.ExecuteAsync(new DeleteStudentAttendanceOccurence(request.Guid));

            if (rowsAffected.NoRowsAreUpdated())
            {
                throw await _dataAccess.FetchAsync(new IsStudentAttendanceOccurenceExisting(request.Guid)) ?
                    new ExpectationFailedException(nameof(DeleteStudentAttendanceOccurenceRequest))
                    : new DoesNotExistException(typeof(StudentAttendanceOccurence), (request.Guid, nameof(request.Guid)));
            }
        }
    }
}
